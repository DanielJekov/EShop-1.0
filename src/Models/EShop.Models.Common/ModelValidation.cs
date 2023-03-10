namespace EShop.Models.Common
{
    using System;

    public static class ModelValidation
    {
        public static class RegisterInputModel
        {

            public const string EmptyFields = "Полето е задължително";

            public const int NameMinLength = 1;
            public const int NameMaxLength = 20;


        }
    }
}
