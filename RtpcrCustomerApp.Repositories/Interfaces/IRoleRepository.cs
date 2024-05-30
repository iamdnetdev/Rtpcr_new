using RtpcrCustomerApp.BusinessModels.DBO;
using System.Collections.Generic;

namespace RtpcrCustomerApp.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<RoleAccessDetails> GetRoleAccessDetails();
    }
}
