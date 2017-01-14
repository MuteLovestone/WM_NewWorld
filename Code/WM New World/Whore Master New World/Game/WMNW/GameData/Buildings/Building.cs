using System;
using System.Xml;
using Limit = WMNW.Systems.LimitManager;
using cfg = WMNW.Systems.ConfigManager;

namespace WMNW.GameData.Buildings
{
    public class Building
    {
        #region Fields

        private string _name = "";
        private BuildingType _type = BuildingType.House;
        private int _filth = -500;
        private int _fame = 0;
        private int _custHappiness = 0;
        private int _securityLevel = 0;
        private int _disposition = 0;
        private int _suspicion = 0;
        private int _beasts = 0;
        private int _totalRooms = 5;
        private int _usedRooms = 0;

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Filth
        {
            get
            {
                return _filth;
            }
            set
            {
                _filth = Limit.LimitBuildingFilth ( value );
            }
        }

        public int Fame
        {
            get
            {
                return _fame;
            }
            set
            {
                _fame = Limit.LimitBuildingFame ( value );
            }
        }

        public string Type
        {
            get
            {
                return _type.ToString ();
            }
        }

        public int Suspicion
        {
            get
            {
                return _suspicion;
            }
            set
            {
                _suspicion = value;
            }
        }

        public int Disposition
        {
            get
            {
                return _disposition;
            }
            set
            {
                _disposition = value;
            }
        }

        public int SecurityLevel
        {
            get
            {
                return _securityLevel;
            }
            set
            {
                _securityLevel = value;
            }
        }

        public int Happiness
        {
            get
            {
                return _custHappiness;
            }
            set
            {
                _custHappiness = Limit.LimitBuildingHappiness ( value );
            }
        }

        public int Beasts
        {
            get
            {
                return _beasts;
            }
            set
            {
                _beasts = value;
            }
        }

        #endregion

        #region Constructor

        public Building ()
        {
        }

        public Building ( string name, BuildingType type = BuildingType.House )
        {
            _name = name;
            _type = type;
        }

        public Building ( XmlNode node )
        {
            Load ( node );
        }

        #endregion

        #region Game Logic

        public void AddGirl()
        {
            _usedRooms++;
        }

        public void RemoveGirl()
        {
            _usedRooms--;
        }

        public bool HasRoom()
        {
            return ( _totalRooms > _usedRooms );
        }

        public void RemoveAllGirls()
        {
            _usedRooms = 0;
        }

        #endregion

        #region Static Game Logic

        public static string GetCleanRating( int filth )
        {
            return filth.ToString ();
        }

        public static string GetFameRating( int fame )
        {
            string fameString = "";
            if ( fame >= 90 )
                fameString = "World Renowned";
            else if ( fame >= 80 )
                fameString = "Famous";
            else if ( fame >= 70 )
                fameString = "Well Known";
            else if ( fame >= 60 )
                fameString = "Talk of the Town";
            else if ( fame >= 50 )
                fameString = "Sumewhat known";
            else if ( fame >= 30 )
                fameString = "Mostly Unknown";
            else
                fameString = "Unknown";
            if ( cfg.Debug.ShowNumbers )
                fameString += " ( " + fame.ToString () + " )";
            return fameString;
        }

        public static string GetHappinessText( int happiness )
        {
            string hRating = "";
            if ( happiness >= 80 )
                hRating = "High";
            else if ( happiness < 40 )
                hRating = "Low";
            else
                hRating = "Medium";
            if ( cfg.Debug.ShowNumbers )
                hRating += " ( " + happiness.ToString () + " )";
            return hRating;
        }

        public static BuildingJobs GetJobs( string buildingType )
        {
            BuildingJobs bi = new BuildingJobs ();
            if ( buildingType == BuildingType.House.ToString () )
            {
                RoomJobs rooms = new RoomJobs ( "House" );
                rooms.Jobs.Add ( new Job ( "Free Time", "JobFreeTime" ) );
                rooms.Jobs.Add ( new Job ( "Head Girl", "JobHeadGirl" ) );
                rooms.Jobs.Add ( new Job ( "Recruiter", "JobHouseRecruiter" ) );
                rooms.Jobs.Add ( new Job ( "Bed Warmer", "JobBedWarmer" ) );
                rooms.Jobs.Add ( new Job ( "House Cook", "JobHouseCook" ) );
                rooms.Jobs.Add ( new Job ( "Clean House", "JobHouseMaid" ) );
                bi.Rooms.Add ( rooms );
                rooms = new RoomJobs ( "Training" );
                rooms.Jobs.Add ( new Job ( "Personal Training", "JobPersonalTraining" ) );
                rooms.Jobs.Add ( new Job ( "Fake Orgasm Expert", "JobFakeOrgasm" ) );
                rooms.Jobs.Add ( new Job ( "SO Straight", "JobSOStraight" ) );
                rooms.Jobs.Add ( new Job ( "SO Bisexual", "JobSOBisexual" ) );
                rooms.Jobs.Add ( new Job ( "SO Lesbian", "JobSOLesbian" ) );
                rooms.Jobs.Add ( new Job ( "House Pet", "Job House Pet" ) );
                bi.Rooms.Add ( rooms );
            }
            else if ( buildingType == BuildingType.Brothel.ToString () )
            {
                #region General Building
                RoomJobs general = new RoomJobs ( "General" );
                general.Jobs.Add ( new Job ( "Free Time", "JobFreeTime" ) );
                general.Jobs.Add ( new Job ( "Practice Skills", "JobPracticeSkills" ) );
                general.Jobs.Add ( new Job ( "Cleaning", "JobBrothelCleaning" ) );
                general.Jobs.Add ( new Job ( "Security", "JobBrothelSecurity" ) );
                general.Jobs.Add ( new Job ( "Advertising", "JobBrothelAdvertising" ) );
                general.Jobs.Add ( new Job ( "Customer Service", "JobCustService" ) );
                general.Jobs.Add ( new Job ( "Matron", "JobMatron" ) );
                general.Jobs.Add ( new Job ( "Torturer", "JobTorturer" ) );
                general.Jobs.Add ( new Job ( "Explore Catacombs", "JobExploreCatacombs" ) );
                general.Jobs.Add ( new Job ( "Beast Carer", "JobBeastCarer" ) );
                bi.Rooms.Add ( general );
                #endregion
                #region Bar
                RoomJobs bar = new RoomJobs ( "Bar" );
                #endregion
                #region Gamble
                RoomJobs gamble = new RoomJobs ( "Gambling Hall" );
                #endregion
                #region Strip Club
                RoomJobs StripClub = new RoomJobs ( "Strip Club" );
                #endregion
                #region Brothel
                RoomJobs Brothel = new RoomJobs ( "Brothel" );
                #endregion
            }
            return bi;
        }

        #endregion

        #region Save/Load Logic

        public void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Building" );
            wr.WriteElementString ( "Name", _name );
            wr.WriteElementString ( "Type", ( byte )_type );
            wr.WriteElementString ( "Filth", _filth );
            wr.WriteElementString ( "Fame", _fame );
            wr.WriteElementString ( "CustHappiness", _custHappiness );
            wr.WriteElementString ( "SecurityLevel", _securityLevel );
            wr.WriteElementString ( "Disposition", _disposition );
            wr.WriteElementString ( "Suspicion", _suspicion );
            wr.WriteElementString ( "Beasts", _beasts );
            wr.WriteElementString ( "TotalRooms", _totalRooms );
            wr.WriteEndElement ();
        }

        public void Load( XmlNode node )
        {
            _name = node [ "Name" ].GetText ();
            _type = ( BuildingType )node [ "Type" ].ConvertToByte ();
            _filth = node [ "Filth" ].ConverToInt ();
            _fame = node [ "Fame" ].ConverToInt ();
            _custHappiness = node [ "CustHappiness" ].ConverToInt ();
            _securityLevel = node [ "SecurityLevel" ].ConverToInt ();
            _disposition = node [ "Disposition" ].ConverToInt ();
            _suspicion = node [ "Susupicion" ].ConverToInt ();
            _beasts = node [ "Beasts" ].ConverToInt ();
            _totalRooms = node [ "TotalRooms" ].ConverToInt ();
        }

        #endregion
    }
}

