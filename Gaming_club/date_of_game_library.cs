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
    
    public partial class date_of_game_library
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public date_of_game_library()
        {
            this.players_of_game_library = new HashSet<players_of_game_library>();
        }
    
        public int id { get; set; }
        public int id_game { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<int> min_number_of_palyers { get; set; }
        public Nullable<int> max_number_of_palyers { get; set; }
        public Nullable<int> min_age { get; set; }
        public int id_user_data { get; set; }
    
        public virtual game game { get; set; }
        public virtual User_data User_data { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<players_of_game_library> players_of_game_library { get; set; }
    }
}
