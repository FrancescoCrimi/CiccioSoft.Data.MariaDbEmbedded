namespace CiccioSoft.Interop.MariaDb.Native
{
    internal unsafe partial struct st_mysql_perm_bind
    {
        [NativeTypeName("ps_field_fetch_func")]
        public delegate* unmanaged[Cdecl]<st_mysql_bind*, st_mysql_field*, byte**, void> func;

        public int pack_len;

        [NativeTypeName("unsigned long")]
        public uint max_len;
    }
}
