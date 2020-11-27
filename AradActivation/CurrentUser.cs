using System;
using DepartmentDal.Classes;

namespace AradActivation
{
    public static class CurrentUser
    {
        public static UserBussines User { get; set; }
        public static DateTime LastVorrod { get; set; }
    }
}