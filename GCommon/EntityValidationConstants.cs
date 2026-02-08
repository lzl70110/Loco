namespace Loco.GCommon;

public static class EntityValidationConstants
    {
    public static class Locomotive
        {
        public const int LocomotiveNumberLength = 6;
        public const string LocomotiveNumberPattern = @"^[0-9]{2}\-[0-9]{3}$";
        public const int CreatedByMinLength = 3;
        public const int CreatedByMaxLength = 10;
        public const double MinFuelCapacity = 0.1;
        public const double MaxFuelCapacity = 5000.0;
        public const string Dec  = "decimal(9,2)";
        public const string DateTimeFormat = "dd-MM-yyyy HH:mm";
        public const int NoteMaxLength = 1000;

        }
    }
