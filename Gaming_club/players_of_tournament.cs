//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gaming_club
{
    using System;
    using System.Collections.Generic;
    
    public partial class players_of_tournament
    {
        public int id { get; set; }
        public int id_tournament { get; set; }
        public int id_user_data { get; set; }
        public Nullable<System.DateTime> reg_date { get; set; }
        public int id_status { get; set; }
    
        public virtual status status { get; set; }
        public virtual tournament tournament { get; set; }
        public virtual User_data User_data { get; set; }
    }
}