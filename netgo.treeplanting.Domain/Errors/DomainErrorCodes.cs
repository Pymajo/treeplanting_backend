using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Errors
{
    public class DomainErrorCodes
    {
        public const string InsufficientPermissions = "INSUFFICIENT_PERMISSIONS";
        public const string ObjectDoesNotExist = "OBJECT_DOES_NOT_EXIST";
        public const string CommitError = "COMMIT_FAILED";
        public const string ObjectAllreadyExist = "OBEJCT_ALLREADY_EXIST";

        public const string UserInvalidId = "INVALID_ID";
        public const string UserNoID = "NO_ID";
        public const string UserInvalidEmailRegistered = "INVALID_EMAILREGISTERED";
        public const string UserInvalidEmail = "INVALID_EMAIL";
        public const string UserInvalidUserName = "INVALID_USER_NAME";
        public const string UserInvalidPasswordHash = "INVALID_PASSWORDHASH";
        public const string UserInvalidIsAdmin = "INVALID_ISADMIN";
        public const string InvalidPromoteAdmin = "INVALID_PROMOTREADMIN";
        public const string InvalidDemoteAdmin = "INVALID_DEMOTEADMIN";
        public const string UserIsNotAdmin = "USER_IS_NOT_ADMIN";
        public const string UserIsNotTreecoinsDeterminer = "USER_IS_NOT_TREECOINS_DETERMINER";
        public const string UserIsNotPlantingOfficer = "USER_IS_NOT_PLANTING_OFFICER";
        public const string UserIsNotPollManager = "USER_IS_NOT_POLL_MANAGER";
        public const string UserIsNotSeedlingsManager = "USER_IS_NOT_SEEDLINGS_MANAGER";
        public const string UserIsAlreadyAdmin = "USER_IS_ALREADY_ADMIN";
        public const string UserIsAlreadyTreecoinsDeterminer = "USER_IS_ALREADY_TREECOINS_DETERMINER";
        public const string UserIsAlreadyPlantingOfficer = "USER_IS_ALREADY_PLANTING_OFFICER";
        public const string UserIsAlreadyPollManager = "USER_IS_ALREADY_POLL_MANAGER";
        public const string UserIsAlreadySeedlingsManager = "USER_IS_ALREADY_SEEDLINGS_MANAGER";

        public const string SeedlingInvalidId = "INVALID_ID";

        public const string PlantingPlaceInvalidId = "INVALID_ID";

    }
}
