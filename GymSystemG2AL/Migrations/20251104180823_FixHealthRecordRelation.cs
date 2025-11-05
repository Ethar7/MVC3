using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemG2AL.Migrations
{
    /// <inheritdoc />
    public partial class FixHealthRecordRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_HealthRecords_HealthRecordId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Trainer_TrainerId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Members_HealthRecordId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HealthRecords");

            migrationBuilder.RenameTable(
                name: "Trainer",
                newName: "Trainers");

            migrationBuilder.RenameIndex(
                name: "IX_Trainer_Phone",
                table: "Trainers",
                newName: "IX_Trainers_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Trainer_Email",
                table: "Trainers",
                newName: "IX_Trainers_Email");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "HealthRecords",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                table: "HealthRecords",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "HealthRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_MemberId",
                table: "HealthRecords",
                column: "MemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_Members_MemberId",
                table: "HealthRecords",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Trainers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_Members_MemberId",
                table: "HealthRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Trainers_TrainerId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_MemberId",
                table: "HealthRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "HealthRecords");

            migrationBuilder.RenameTable(
                name: "Trainers",
                newName: "Trainer");

            migrationBuilder.RenameIndex(
                name: "IX_Trainers_Phone",
                table: "Trainer",
                newName: "IX_Trainer_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Trainers_Email",
                table: "Trainer",
                newName: "IX_Trainer_Email");

            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "HealthRecords",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                table: "HealthRecords",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HealthRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HealthRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainer",
                table: "Trainer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Members_HealthRecordId",
                table: "Members",
                column: "HealthRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_HealthRecords_HealthRecordId",
                table: "Members",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Trainer_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
