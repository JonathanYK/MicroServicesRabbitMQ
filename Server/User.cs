using System;

namespace Server
{
    class User
    {
        public String name;
        public String date;
        public Double age;
        public String profession;


        //Constructor
        public User (String name, String date, Double age, String profession)
        {
            this.name = name;
            this.date = date;
            this.age = age;
            this.profession = profession;
        }
    }
}
