//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinic.DAL.Model
{
    using System;
    
    public partial class SP_Cities_GetData_Result
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<bool> CityIsActive { get; set; }
        public int CountryFk { get; set; }
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
        public string CountryCode { get; set; }
    }
}