using PL.Models;
using System.Collections.Generic;

namespace PL.RoleManager
{
    public static class RolesManager
    {
        public static bool IsUser(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "user")
                    return true;
            }
            return false;
        }
        public static bool IsReciver(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "receiver")
                    return true;
            }
            return false;
        }
        public static bool IsPizzaManager(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "pizzaManager")
                    return true;
            }
            return false;
        }
        public static bool IsEmployeeManager(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "employeeManager")
                    return true;
            }
            return false;
        }
        public static bool IsModerator(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "moderator")
                    return true;
            }
            return false;
        }
        public static bool IsAccountant(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "accountant")
                    return true;
            }
            return false;
        }
        public static bool IsClientManager(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "clientManager")
                    return true;
            }
            return false;
        }
        public static bool IsExecutor(IEnumerable<RoleViewModel> roles)
        {
            foreach (var item in roles)
            {
                if (item.Name == "executor")
                    return true;
            }
            return false;
        }
    }
}