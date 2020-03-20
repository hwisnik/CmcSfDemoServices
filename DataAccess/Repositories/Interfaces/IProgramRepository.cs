using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Shared.Entities.DTO.Program;
using Shared.Entities._SearchCriteria.Customer;

namespace DataAccess.Repositories.Interfaces
{
    public interface IProgramRepository
    {
        Task<Program> GetProgramFromGuid(Guid ProgramGuid, LogCommand logCommand);
        Task<Program> GetProgramFromCustomerGuid(Guid CustomerGuid, LogCommand logCommand);
        Task<IEnumerable<Program>> FindProgram(SearchProgram _SearchProgram, LogCommand logCommand);
        Task<IEnumerable<Subprogram>> GetProgramAndSubprogramBySfSubProgramId(string sfSubProgramId, LogCommand logCommand);

        Task<Program> GetProgramFromSFID(string SFProgramRecordID, LogCommand logCommand);
        Task<Subprogram> GetSubProgramSFID(string SFSubprogramRecordID, LogCommand logCommand);

        Task<IEnumerable<Program>> GetPrograms(LogCommand logCommand);
        Task<IEnumerable<Subprogram>> GetSubPrograms(LogCommand logCommand);
    }
}
