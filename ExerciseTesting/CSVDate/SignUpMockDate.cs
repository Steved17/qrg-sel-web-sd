using System;

namespace ExerciseTesting.CSVDate
{
    class SignUpMockDate
    {
        //Should have properties which correspond to the Column Names in the file
        //i.e. CommonName,FormalName,TelephoneCode,CountryCode
        public String id { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String mobile { get; set; }
        public String email { get; set; }
        public String password { get; set; }

    }
}
