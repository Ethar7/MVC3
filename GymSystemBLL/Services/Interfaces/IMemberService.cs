using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemBLL.ViewModels;
using GymSystemG2AL.Entities;


namespace GymSystemBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMembers(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int id);

        //Get HealthReco

        HealthViewModel? GetMemberHealthRecordDetails(int MemberId);

        // GetMemberId To Ubdate view
        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        // Apply Ubdate 

        bool UpdateMemberDetails(int id, MemberToUpdateViewModel updateMember);


        //Remove
        bool RemoveMember(int MemberId);
    }
}