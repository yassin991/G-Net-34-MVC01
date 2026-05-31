using GymSystem.BLL.Services.Interfaces;
using GymSystem.BLL.ViewModels.MemberViewModels;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Entities.Enums;
using GymSystem.DAL.Repositories;
using GymSystem.DAL.Repositories.Classes;
using GymSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Services.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IGenericrepository<Member> memberRepository;
        private readonly IGenericrepository<Membership> membershipRepository;
        private readonly IGenericrepository<Plan> PlanRepository;
        private readonly IGenericrepository<HealthRecord> healthRecordRepository;
        private readonly IGenericrepository<Booking> bookingRepository;

        public MemberServices(IGenericrepository<Member> memberRepository,
            IGenericrepository<Membership> membershipRepository
            , IGenericrepository<Plan>PlanRepository, IGenericrepository<HealthRecord> HealthRecordRepository
            , IGenericrepository<Booking>bookingRepository)
        {
            this.memberRepository = memberRepository;
            this.membershipRepository = membershipRepository;
            this.PlanRepository = PlanRepository;
            this.healthRecordRepository = HealthRecordRepository;
            this.bookingRepository = bookingRepository;
        }

        //get


        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await memberRepository.GetAll(false, ct);

            if (!members.Any()) return [];
            var membersViewModel = members.Select(m => new MemberViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString()
            });

            return membersViewModel;

        }

        public async Task<MemberViewModel?> GetMemberDetailsAsync(int memberId, CancellationToken ct = default)
        {
            //Member + Membership + Plan
            var member = await memberRepository.GetById(memberId, ct);

            if (member is null) return null;

            var MemberVM = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };
            var ActiveMemberShip = await membershipRepository.FirstOrDefaultAsync(mb=>mb.MemberId==memberId && mb.EndDate> DateTime.Now,false,ct);
            if (ActiveMemberShip is not null)
            {
                var ActivePlan = await PlanRepository.GetById(ActiveMemberShip.PlanId, ct);

                MemberVM.PlanName = ActivePlan?.Name;
                MemberVM.MembershipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                MemberVM.MembershipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
            }
            return MemberVM;
        }

        public async Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int memberId, CancellationToken ct = default)
        {
            var Record = await healthRecordRepository.FirstOrDefaultAsync(r => r.MemberId == memberId, false, ct);
            if (Record is null) return null;
            return new HealthRecordViewModel()
            {
                Weight = Record.weight,
                Height = Record.Height,
                BloodType = Record.BloodType,
                Note = Record.Note,
            };
        }

        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId, CancellationToken ct = default)
        {
            var member = await memberRepository.GetById(memberId, ct);

            if (member is null) return null;

            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Street = member.Address.Street,
                City = member.Address.City,
                BuildingNumber = member.Address.BuildingNumber,
                Photo = member.Photo
            };
        }
        //post
        public async Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var emailExsists = await memberRepository.AnyAsync(m => m.Email == model.Email, ct);
            var phoneExsists = await memberRepository.AnyAsync(m => m.Phone == model.Phone, ct);

            if (emailExsists || phoneExsists) return false;
            var Member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street,
                },
                HealthRecord = new HealthRecord()
                {
                    weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note,
                }

            };
            memberRepository.Add(Member);
            var Result = await memberRepository.CompleteAsync();
            return Result > 0;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            var member = await memberRepository.GetById(id, ct);

            if (member is null) return false;

            //Email == member.Email && id != member.id

            if (await memberRepository.AnyAsync(m => m.Email == model.Email && m.Id != id)) return false;
            if (await memberRepository.AnyAsync(m => m.Phone == model.Phone && m.Id != id)) return false;

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.City = model.City;
            member.Address.Street = model.Street;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.UpdatedAt = DateTime.Now;

            memberRepository.Update(member);

            var Result = await memberRepository.CompleteAsync();

            return Result > 0;



        }

        public async Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default)
        {

            var HasFutureSessions = await bookingRepository.AnyAsync(b => b.MemberId == memberId && b.Session.EndDate >DateTime.Now);

if (HasFutureSessions) return false;

            memberRepository.Delete(memberId);

            var Result = await memberRepository.CompleteAsync();

return Result > 0;



        }
    }
}
