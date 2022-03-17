namespace BlazorControlCefa.Application.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static class Dashboard
        {
            public const string View = "Permissions.Dashboard.View";
            public const string Create = "Permissions.Dashboard.Create";
            public const string Edit = "Permissions.Dashboard.Edit";
            public const string Delete = "Permissions.Dashboard.Delete";
        }

        public static class Person
        {
            public const string View = "Permissions.Persons.View";
            public const string Create = "Permissions.Persons.Create";
            public const string Edit = "Permissions.Persons.Edit";
            public const string Delete = "Permissions.Persons.Delete";
            public const string Special = "Permissions.Persons.Special";
        }
    }
}
