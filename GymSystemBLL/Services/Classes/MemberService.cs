using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
namespace GymSystemBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IGenaricRepository<Member> memberRepository;
        private readonly IGenaricRepository<MemberShip> memberShipRepository;
        private readonly IPlanRepository planRepository;
        private readonly IGenaricRepository<HealthRecord> healthRecordRepository;
        private readonly IGenaricRepository<MemberSession> memberSessionRepository;

        public MemberService(IGenaricRepository<Member> MemberRepository
        , IGenaricRepository<MemberShip> memberShipRepository
        , IPlanRepository planRepository
        , IGenaricRepository<HealthRecord> healthRecordRepository
        , IGenaricRepository <MemberSession> memberSessionRepository)

        {
            memberRepository = MemberRepository;
            this.memberShipRepository = memberShipRepository;
            this.planRepository = planRepository;
            this.healthRecordRepository = healthRecordRepository;
            this.memberSessionRepository = memberSessionRepository;
        }

        public bool CreateMembers(CreateMemberViewModel createMember)
        {
            try
            {
                
            //check if phone or Email are Unique

            if (IsEmailExist(createMember.Email) || IsPhoneExist(createMember.Phone)) return false;

                var member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,

                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,

                    Address = new Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthViewModel.Height,
                        Weight = createMember.HealthViewModel.Weight,
                        BloodType = createMember.HealthViewModel.BloodType,
                        Note = createMember.HealthViewModel.Note
                    }
                };
                return memberRepository.Add(member) > 0;
        }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            # region First way of Mapping
            // var Members = memberRepository.GetAll() ?? [];
            // var Members = memberRepository.GetAll();
            // if (Members is null || Members.Any()) return [];

            // var MemberViewModel = new List<MemberViewModel>();
            // foreach (var Member in Members)
            // {
            //     var memberViewModel = new MemberViewModel()
            //     {
            //         Id = Member.Id,
            //         Name = Member.Name,
            //         Email = Member.Email,
            //         Phone = Member.Phone,
            //         Gender = Member.Gender.ToString(),
            //     };
            //     MemberViewModel.Add(memberViewModel);
            // }
            // return MemberViewModel;
            #endregion

             
            var Members = memberRepository.GetAll();
            if (Members is null || Members.Any()) return [];

            var MemberViewModel = Members.Select(X => new MemberViewModel
            {
                Id = X.Id,
                Name = X.Name,
                Phone = X.Phone,
                Photo = X.Photo,
                Gender = X.Gender.ToString()
            });
            return MemberViewModel;
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            // Interface IPlan Repository
            // inject for plan repo and MemberShip Repo

            var Member = memberRepository.GetBYId(MemberId);

            if (Member == null) return null;

            var ViewModel = new MemberViewModel()
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Photo = Member.Photo,
                Gender = Member.Gender.ToString(),
                DateOfBirth = Member.DateOfBirth.ToShortDateString(),
                Address = $"{Member.Address.BuildingNumber} - {Member.Address.Street} - {Member.Address.City}"
            };
            var ActiveMemberShip = memberShipRepository.GetAll(X => X.MemberId == MemberId && X.Status == "Active")
            .FirstOrDefault();

            if (ActiveMemberShip is not null) // start end date
            {
                ViewModel.MemberShipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModel.MemberShipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
                // plans

                var Plan = planRepository.GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = Plan?.Name;
            }
            return ViewModel;
        }

        public HealthViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = healthRecordRepository.GetBYId(MemberId);

            if (MemberHealthRecord is null) return null;

            return new HealthViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Note,
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var Member = memberRepository.GetBYId(MemberId);
            if (Member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = Member.Email,
                Phone = Member.Phone,
                Photo = Member.Photo,
                Name = Member.Name,
                BuildingNumber = Member.Address.BuildingNumber,
                City = Member.Address.City,
                Street = Member.Address.Street,
            };
        }

        public bool RemoveMember(int MemberId)
        {
            var Member = memberRepository.GetBYId(MemberId);
            if (Member is null) return false;

            var HasActiveMemberSession = memberSessionRepository.GetAll(X => X.MemberId == MemberId && X.Session.StartDate > DateTime.Now).Any();

            if (HasActiveMemberSession) return false;

            // Remove

            var Membership = memberShipRepository.GetAll(X => X.MemberId == MemberId);

            try
            {
                if (Membership.Any())
                {
                    foreach (var member in Membership)
                    {
                        memberShipRepository.Delete(member);
                    }
                }
                return memberRepository.Delete(Member) > 0;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool UpdateMemberDetails(int id, MemberToUpdateViewModel updateMember)
        {
            try
            {
                
            if (IsEmailExist(updateMember.Email) || IsPhoneExist(updateMember.Phone)) return false;

            var Member = memberRepository.GetBYId(id);
            if (Member is null) return false;

            Member.Email = updateMember.Email;
            Member.Phone = updateMember.Phone;
            Member.Address.BuildingNumber = updateMember.BuildingNumber;
            Member.Address.Street = updateMember.Street;
            Member.Address.City = updateMember.City;
            Member.UpdatedAt = DateTime.Now;

            return memberRepository.Update(Member) > 0;
            }

            catch (System.Exception)
            {
                return false;
            }
        }
        #region  HelperMethods
        private bool IsEmailExist(string email)
        {
            return memberRepository.GetAll(X => X.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return memberRepository.GetAll(X => X.Phone == phone).Any();
        }

        #endregion
    }
}