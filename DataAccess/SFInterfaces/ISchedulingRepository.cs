using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Scheduling;

namespace DataAccess.SFInterfaces
{
    public interface ISchedulingRepository
    {
        // Read Enums
        Task<IEnumerable<Scheduling>> GetSchedulingEnum(LogCommand logCommand);
        Task<IEnumerable<LkpSchedulingType>> GetLkpSchedulingTypeEnum(LogCommand logCommand);
        Task<IEnumerable<UsernameMappingSFtoAD>> GetUsernameMappingSFtoADEnum(LogCommand logCommand);
        Task<IEnumerable<LKPWorkOrderScoreMapping>> GetWorkOrderScoreWeightingEnum(LogCommand logCommand);
        Task<IEnumerable<SimpleScheduling>> GetSimpleScheduling(string SfRecordId, LogCommand logCommand);
        

        // Create
        Task<int> CreateNewSchedulingRecord(Scheduling scheduling, LogCommand logCommand);

        Task<int> CreateNewUsernameMappingSFtoADRecord(UsernameMappingSFtoAD usernameMappingSFtoAD,
            LogCommand logCommand);
        Task<int> CreateSimpleSchedulingRecordFirst(SimpleScheduling scheduling, LogCommand logCommand);
        Task<int> CreateSimpleSchedulingRecordReschedule(SimpleScheduling scheduling, LogCommand logCommand);
    }
}
