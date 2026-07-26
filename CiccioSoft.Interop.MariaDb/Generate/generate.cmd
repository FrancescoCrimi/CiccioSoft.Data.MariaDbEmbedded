call ClangSharpPInvokeGenerator "@generate1.rsp" 		& REM mysql.h
call ClangSharpPInvokeGenerator "@generate2.rsp"		& REM mariadb_com.h
call ClangSharpPInvokeGenerator "@generate3.rsp"		& REM mariadb_version
call ClangSharpPInvokeGenerator "@generate4.rsp"		& REM ma_list
:: call ClangSharpPInvokeGenerator "@generate5.rsp"		& REM mariadb_ctype
call ClangSharpPInvokeGenerator "@generate6.rsp"		& REM mariadb_stmt
