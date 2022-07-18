�k
kD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLBase.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{ 
public 

class 
DLBase 
< 
TEntity 
,  
TDTO! %
>% &
:' (
IDLBase) 0
<0 1
TEntity1 8
,8 9
TDTO9 =
>= >
where? D
TEntityE L
:M N
classO T
{ 
	protected 
WhereHouseContext #
_context$ ,
;, -
	protected 
String 
	tableName "
=# $
$str% '
;' (
	protected 
DbSet 
< 
TEntity 
>  
_dbSet! '
;' (
	protected 
readonly 
IMapper "
_mapper# *
;* +
public 
DLBase 
( 
WhereHouseContext '
context( /
,/ 0
IMapper1 8
mapper9 ?
)? @
{   	
_context!! 
=!! 
context!! 
;!! 
	tableName"" 
="" 
typeof"" 
("" 
TEntity"" &
)""& '
.""' (
Name""( ,
;"", -
_mapper## 
=## 
mapper## 
;## 
}$$ 	
public&& 
bool&& 
Delete&& 
(&& 
TEntity&& "
entity&&# )
)&&) *
{'' 	
var(( 
res(( 
=(( 
$num(( 
;(( 
var)) 
idKey)) 
=)) 
$")) 
{)) 
	tableName)) %
}))% &
$str))& (
"))( )
;))) *
var** 
Id** 
=** 
entity** 
.** 

<**) *
TEntity*** 1
>**1 2
(**2 3
idKey**3 8
)**8 9
;**9 :
_dbSet++ 
=++ 
_context++ 
.++ 
Set++ !
<++! "
TEntity++" )
>++) *
(++* +
)+++ ,
;++, -
var,, 
record,, 
=,, 
_dbSet,, 
.,,  
Find,,  $
(,,$ %
Guid,,% )
.,,) *
Parse,,* /
(,,/ 0
Id,,0 2
.,,2 3
ToString,,3 ;
(,,; <
),,< =
),,= >
),,> ?
;,,? @
if-- 
(-- 
record-- 
!=-- 
null-- 
)-- 
{.. 
_dbSet// 
.// 
Remove// 
(// 
record// $
)//$ %
;//% &
res00 
=00 
_context00 
.00 
SaveChanges00 *
(00* +
)00+ ,
;00, -
}11 
return22 
(22 
res22 
>22 
$num22 
?22 
true22 "
:22# $
false22% *
)22* +
;22+ ,
}33 	
public55 
IEnumerable55 
<55 
TDTO55 
>55  
GetAll55! '
(55' (
)55( )
{66 	
_dbSet77 
=77 
_context77 
.77 
Set77 !
<77! "
TEntity77" )
>77) *
(77* +
)77+ ,
;77, -
var88 
obj88 
=88 
(88 
TDTO88 
)88 
typeof88 "
(88" #
TDTO88# '
)88' (
.88( )
GetConstructor88) 7
(887 8
new888 ;
Type88< @
[88@ A
$num88A B
]88B C
)88C D
.88D E
Invoke88E K
(88K L
new88L O
object88P V
[88V W
$num88W X
]88X Y
)88Y Z
;88Z [
var>> 
res>> 
=>> 
_mapper>> 
.>> 
Map>> !
<>>! "
List>>" &
<>>& '
TEntity>>' .
>>>. /
,>>/ 0
IEnumerable>>0 ;
<>>; <
TDTO>>< @
>>>@ A
>>>A B
(>>B C
_dbSet>>C I
.>>I J
ToList>>J P
(>>P Q
)>>Q R
)>>R S
;>>S T
return?? 
res?? 
;?? 
}@@ 	
publicBB 
virtualBB 
TEntityBB 
GetByIDBB &
(BB& '
stringBB' -
IdBB. 0
)BB0 1
{CC 	
_dbSetDD 
=DD 
_contextDD 
.DD 
SetDD !
<DD! "
TEntityDD" )
>DD) *
(DD* +
)DD+ ,
;DD, -
varEE 
resEE 
=EE 
_dbSetEE 
.EE 
FindEE  
(EE  !
GuidEE! %
.EE% &
ParseEE& +
(EE+ ,
IdEE, .
)EE. /
)EE/ 0
;EE0 1
returnFF 
resFF 
;FF 
}GG 	
publicII 
boolII 
InsertII 
(II 
TEntityII "
entityII# )
)II) *
{JJ 	
_dbSetKK 
=KK 
_contextKK 
.KK 
SetKK !
<KK! "
TEntityKK" )
>KK) *
(KK* +
)KK+ ,
;KK, -
_dbSetLL 
.LL 
AddLL 
(LL 
entityLL 
)LL 
;LL 
varMM 
resMM 
=MM 
_contextMM 
.MM 
SaveChangesMM *
(MM* +
)MM+ ,
;MM, -
returnNN 
(NN 
resNN 
>NN 
$numNN 
?NN 
trueNN "
:NN# $
falseNN% *
)NN* +
;NN+ ,
}OO 	
publicQQ 
boolQQ 
UpdateQQ 
(QQ 
TEntityQQ "
entityQQ# )
)QQ) *
{RR 	
varSS 
resSS 
=SS 
$numSS 
;SS 
varTT 
idKeyTT 
=TT 
$"TT 
{TT 
	tableNameTT %
}TT% &
$strTT& (
"TT( )
;TT) *
varUU 
IdUU 
=UU 
entityUU 
.UU 

<UU) *
TEntityUU* 1
>UU1 2
(UU2 3
idKeyUU3 8
)UU8 9
;UU9 :
_dbSetVV 
=VV 
_contextVV 
.VV 
SetVV !
<VV! "
TEntityVV" )
>VV) *
(VV* +
)VV+ ,
;VV, -
varWW 
recordWW 
=WW 
_dbSetWW 
.WW  
FindWW  $
(WW$ %
GuidWW% )
.WW) *
ParseWW* /
(WW/ 0
IdWW0 2
.WW2 3
ToStringWW3 ;
(WW; <
)WW< =
)WW= >
)WW> ?
;WW? @
ifXX 
(XX 
recordXX 
!=XX 
nullXX 
)XX 
{YY 
_dbSetZZ 
.ZZ 
AttachZZ 
(ZZ 
recordZZ $
)ZZ$ %
;ZZ% &
record[[ 
=[[ 
entity[[ 
;[[  
res\\ 
=\\ 
_context\\ 
.\\ 
SaveChanges\\ *
(\\* +
)\\+ ,
;\\, -
}]] 
return^^ 
(^^ 
res^^ 
>^^ 
$num^^ 
?^^ 
true^^ "
:^^# $
false^^% *
)^^* +
;^^+ ,
}__ 	
publicaa 
IEnumerableaa 
<aa 
TEntityaa "
>aa" #
GetByKeyaa$ ,
(aa, -
stringaa- 3
keyaa4 7
,aa7 8
stringaa8 >
valueaa? D
)aaD E
{bb 	
IEnumerabledd 
<dd 
TEntitydd 
>dd  
resdd! $
;dd$ %
_dbSetee 
=ee 
_contextee 
.ee 
Setee !
<ee! "
TEntityee" )
>ee) *
(ee* +
)ee+ ,
;ee, -
reshh 
=hh 
_dbSethh 
.hh 

FromSqlRawhh #
(hh# $
$"hh$ &
$strhh& 4
{hh4 5
	tableNamehh5 >
}hh> ?
$strhh? F
{hhF G
keyhhG J
}hhJ K
$strhhK O
{hhO P
valuehhP U
}hhU V
$strhhV W
"hhW X
)hhX Y
;hhY Z
returnjj 
resjj 
;jj 
}ll 	
publicmm 
TEntitymm 
GetOneByKeymm "
(mm" #
stringmm# )
keymm* -
,mm- .
stringmm/ 5
valuemm6 ;
)mm; <
{nn 	
TEntitypp 
respp 
=pp 
nullpp 
;pp 
_dbSetqq 
=qq 
_contextqq 
.qq 
Setqq !
<qq! "
TEntityqq" )
>qq) *
(qq* +
)qq+ ,
;qq, -
restt 
=tt 
_dbSettt 
.tt 

FromSqlRawtt #
(tt# $
$"tt$ &
$strtt& 4
{tt4 5
	tableNamett5 >
}tt> ?
$strtt? F
{ttF G
keyttG J
}ttJ K
$strttK O
{ttO P
valuettP U
}ttU V
$strttV W
"ttW X
)ttX Y
.ttY Z
FirstOrDefaultttZ h
(tth i
)tti j
;ttj k
returnuu 
resuu 
;uu 
}ww 	
publicxx 
	TableNamexx 
GetOneByKeyWithTypexx ,
<xx, -
	TableNamexx- 6
>xx6 7
(xx7 8
PropertyInfoxx8 D
propxxE I
,xxI J
TEntityxxK R
entityxxS Y
)xxY Z
wherexx[ `
	TableNamexxa j
:xxk l
classxxm r
{yy 	
	TableNamezz 
reszz 
=zz 
nullzz  
;zz  !
var{{ 
key{{ 
={{ 
prop{{ 
.{{ 
Name{{ 
;{{  
var|| 
value|| 
=|| 
prop|| 
.|| 
GetValue|| %
(||% &
entity||& ,
)||, -
;||- .
var}} 
table}} 
=}} 
typeof}} 
(}} 
	TableName}} (
)}}( )
.}}) *
Name}}* .
;}}. /
var~~ 
dbSet~~ 
=~~ 
_context~~  
.~~  !
Set~~! $
<~~$ %
	TableName~~% .
>~~. /
(~~/ 0
)~~0 1
;~~1 2
res
�� 
=
�� 
dbSet
�� 
.
�� 

FromSqlRaw
�� "
(
��" #
$"
��# %
$str
��% 4
{
��4 5
table
��5 :
}
��: ;
$str
��; C
{
��C D
key
��D G
}
��G H
$str
��H L
{
��L M
value
��M R
}
��R S
$str
��S T
"
��T U
)
��U V
.
��V W
FirstOrDefault
��W e
(
��e f
)
��f g
;
��g h
return
�� 
res
�� 
;
�� 
}
�� 	
public
�� 
virtual
�� 

�� $
GetByPaging
��% 0
(
��0 1
int
��1 4
page
��5 9
,
��9 :
int
��; >
pageSize
��? G
)
��G H
{
�� 	
_dbSet
�� 
=
�� 
_context
�� 
.
�� 
Set
�� !
<
��! "
TEntity
��" )
>
��) *
(
��* +
)
��+ ,
;
��, -
var
�� 
totalRecords
�� 
=
�� 
_dbSet
�� %
.
��% &
Count
��& +
(
��+ ,
)
��, -
;
��- .
var
�� 
skip
�� 
=
�� 
(
�� 
page
�� 
-
�� 
$num
��  
)
��  !
*
��" #
pageSize
��$ ,
;
��, -
var
�� 
res
�� 
=
�� 
new
�� 

�� '
(
��' (
)
��( )
;
��) *
if
�� 
(
�� 
skip
�� 
>=
�� 
$num
�� 
&&
�� 
pageSize
�� %
>
��& '
$num
��( )
)
��) *
{
�� 
var
�� 
data
�� 
=
�� 
_dbSet
�� !
.
��! "
Skip
��" &
(
��& '
skip
��' +
)
��+ ,
.
��, -
Take
��- 1
(
��1 2
pageSize
��2 :
)
��: ;
;
��; <
if
�� 
(
�� 
typeof
�� 
(
�� 
TEntity
�� "
)
��" #
==
��$ &
typeof
��' -
(
��- .
House
��. 3
)
��3 4
)
��4 5
{
�� 
}
�� 
res
�� 
.
�� 
Data
�� 
=
�� 
new
�� 
{
��  
TotalRecords
��! -
=
��. /
totalRecords
��0 <
,
��< =
Data
��> B
=
��C D
data
��E I
.
��I J
ToList
��J P
(
��P Q
)
��Q R
}
��S T
;
��T U
}
�� 
return
�� 
res
�� 
;
�� 
}
�� 	
}
�� 
}�� �
sD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLConversation.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{
public 

class 
DLConversation 
:  !
DLBase" (
<( )
Conversation) 5
,5 6
ConversationDTO7 F
>F G
,G H
IDLConversationI X
{ 
public 
DLConversation 
( 
WhereHouseContext /
context0 7
,7 8
IMapper9 @
mapperA G
)G H
:I J
baseK O
(O P
contextP W
,W X
mapperY _
)_ `
{ 	
} 	
public 
override 
Conversation $
GetByID% ,
(, -
string- 3
Id4 6
)6 7
{ 	
_dbSet 
= 
_context 
. 
Set !
<! "
Conversation" .
>. /
(/ 0
)0 1
;1 2
var 
data 
= 
_dbSet 
. 
Where #
(# $
x$ %
=>& (
x) *
.* +
ConversationId+ 9
==: <
Guid= A
.A B
ParseB G
(G H
IdH J
)J K
)K L
.L M
IncludeM T
(T U
xU V
=>W Y
xZ [
.[ \
Messages\ d
)d e
.e f
FirstOrDefaultf t
(t u
)u v
;v w
return 
data 
; 
} 	
} 
} �
kD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLFile.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{ 
public

class
DLFile
:
DLBase
<
File
,
FileDTO
>
,
IDLFile
{ 
public 
DLFile 
( 
WhereHouseContext '
context( /
,/ 0
IMapper1 8
mapper9 ?
)? @
:A B
baseC G
(G H
contextH O
,O P
mapperQ W
)W X
{ 	
} 	
} 
} �
lD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLHouse.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{ 
public 

class 
DLHouse 
: 
DLBase !
<! "
House" '
,' (
HouseDTO) 1
>1 2
,2 3
IDLHouse4 <
{ 
public 
DLHouse 
( 
WhereHouseContext (
context) 0
,0 1
IMapper2 9
mapper: @
)@ A
:B C
baseD H
(H I
contextI P
,P Q
mapperR X
)X Y
{ 	
} 	
public 
IEnumerable 
< 
House  
>  !
GetDeepData" -
(- .
). /
{ 	
_dbSet 
= 
_context 
. 
Set !
<! "
House" '
>' (
(( )
)) *
;* +
return 
_dbSet 
. 
Include !
(! "
x" #
=>$ &
x' (
.( )
	UserOwner) 2
)2 3
;3 4
} 	
} 
} �

nD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLMessage.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{
public 

class 
	DLMessage 
: 
DLBase #
<# $
Message$ +
,+ ,

MessageDTO- 7
>7 8
,8 9

IDLMessage: D
{ 
IDLUser 
_dLUser 
; 
public 
	DLMessage 
( 
WhereHouseContext *
context+ 2
,2 3
IMapper4 ;
mapper< B
,B C
IDLUserD K
dLUserL R
)R S
:T U
baseV Z
(Z [
context[ b
,b c
mapperd j
)j k
{ 	
_dLUser 
= 
dLUser 
; 
} 	
public 
object 
GetConversation %
(% &

MessageDTO& 0
message1 8
)8 9
{ 	
return 
null 
; 
}   	
}!! 
}"" �%
kD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLPost.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{ 
public 

class 
DLPost 
: 
DLBase  
<  !
Post! %
,% &
PostDTO' .
>. /
,/ 0
IDLPost1 8
{ 
public 
DLPost 
( 
WhereHouseContext '
context( /
,/ 0
IMapper1 8
mapper9 ?
)? @
:A B
baseC G
(G H
contextH O
,O P
mapperQ W
)W X
{ 	
} 	
public 
override 
Post 
GetByID $
($ %
string% +
Id, .
). /
{ 	
_dbSet 
= 
_context 
. 
Set !
<! "
Post" &
>& '
(' (
)( )
;) *
var 
post 
= 
_dbSet 
. 
Where #
(# $
x$ %
=>& (
x) *
.* +
PostId+ 1
.1 2
ToString2 :
(: ;
); <
=== ?
Id@ B
)B C
. 
Include 
( 
x 
=> 
x 
. 
User "
)" #
.# $
Include$ +
(+ ,
x, -
=>. 0
x1 2
.2 3
House3 8
)8 9
.9 :
ThenInclude: E
(E F
houseF K
=>K M
houseM R
.R S
	HouseTypeS \
)\ ]
. 
Include 
( 
z 
=> 
z 
. 
House #
)# $
.$ %
ThenInclude% 0
(0 1
i1 2
=>2 4
i4 5
.5 6

HouseImage6 @
)@ A
. 
FirstOrDefault 
(  
)  !
;! "
return 
post 
; 
}   	
public!! 
override!! 

GetByPaging!!& 1
(!!1 2
int!!2 5
page!!6 :
,!!: ;
int!!< ?
pageSize!!@ H
)!!H I
{"" 	
_dbSet## 
=## 
_context## 
.## 
Set## !
<##! "
Post##" &
>##& '
(##' (
)##( )
;##) *
var$$ 
totalRecords$$ 
=$$ 
_dbSet$$ %
.$$% &
Count$$& +
($$+ ,
)$$, -
;$$- .
var%% 
skip%% 
=%% 
(%% 
page%% 
-%% 
$num%%  
)%%  !
*%%" #
pageSize%%$ ,
;%%, -
var&& 
res&& 
=&& 
new&& 

(&&' (
)&&( )
;&&) *
if'' 
('' 
skip'' 
>='' 
$num'' 
&&'' 
pageSize'' %
>''& '
$num''( )
)'') *
{(( 
var)) 
data)) 
=)) 
_dbSet)) !
.))! "
Skip))" &
())& '
skip))' +
)))+ ,
.)), -
Take))- 1
())1 2
pageSize))2 :
))): ;
.)); <
Include))< C
())C D
x))D E
=>))F H
x))I J
.))J K
House))K P
)))P Q
.))Q R
ThenInclude))R ]
())] ^
y))^ _
=>))` b
y))c d
.))d e

HouseImage))e o
)))o p
;))p q
data** 
?** 
.** 
Include** 
(** 
x** 
=>**  "
x**# $
.**$ %
House**% *
)*** +
.**+ ,
ThenInclude**, 7
(**7 8
y**8 9
=>**: <
y**= >
.**> ?

HouseImage**? I
)**I J
;**J K
res++ 
.++ 
Data++ 
=++ 
new++ 
{++  
TotalRecords++! -
=++. /
totalRecords++0 <
,++< =
Data++> B
=++C D
data++E I
.++I J
ToList++J P
(++P Q
)++Q R
}++S T
;++T U
},, 
return-- 
res-- 
;-- 
}.. 	
}// 
}00 �
kD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLUser.cs
	namespace
PTDuc
 
.

WhereHouse
.
DL
.

{ 
public 

class 
DLUser 
: 
DLBase  
<  !
User! %
,% &
UserDTO' .
>. /
,/ 0
IDLUser1 8
{ 
public 
DLUser 
( 
WhereHouseContext '
context( /
,/ 0
IMapper1 8
mapper9 ?
)? @
:A B
baseC G
(G H
contextH O
,O P
mapperQ W
)W X
{ 	
} 	
public 
override 
User 
GetByID $
($ %
string% +
Id, .
). /
{ 	
_dbSet 
= 
_context 
. 
Set !
<! "
User" &
>& '
(' (
)( )
;) *
var 
data 
= 
_dbSet 
. 
Where #
(# $
x$ %
=>& (
x) *
.* +
UserId+ 1
==2 4
Guid5 9
.9 :
Parse: ?
(? @
Id@ B
)B C
)C D
.D E
IncludeE L
(L M
xM N
=>O Q
xR S
.S T
AvatarT Z
)Z [
.[ \
FirstOrDefault\ j
(j k
)k l
;l m
return 
data 
; 
} 	
} 
} �
oD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\DLWishlist.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

{ 
public

class

DLWishlist
:
DLBase
<
Wishlist
,
WishlistDTO
>
,
IDLWishlist
{ 
public 

DLWishlist 
( 
WhereHouseContext +
context, 3
,3 4
IMapper5 <
mapper= C
)C D
:E F
baseG K
(K L
contextL S
,S T
mapperU [
)[ \
{ 	
} 	
} 
} �
rD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\DatabaseLayer\Login\DLLogin.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

.+ ,
Login, 1
{
public 

class 
DLLogin 
: 
DLBase  
<  !

LoginParam! +
,+ ,

LoginParam, 6
>6 7
,7 8
IDLLogin9 A
{ 
public 
DLLogin 
( 
WhereHouseContext (
context) 0
,0 1
IMapper2 9
mapper: @
)@ A
:B C
baseD H
(H I
contextI P
,P Q
mapperR X
)X Y
{ 	
} 	
public 
LoginEntity 
Login  
(  !
string! '
Username( 0
)0 1
{ 	
var 
entity 
= 
new 
LoginEntity (
(( )
)) *
;* +
return 
entity 
; 
} 	
public 
int (
UpdatePasswordByEmployeeCode /
(/ 0
string0 6
employeecode7 C
,C D
stringE K
passwordL T
,T U
stringV \
salt] a
)a b
{ 	
var 
rows 
= 
$num 
; 
return 
rows 
; 
}   	
}"" 
}## �
iD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLBase.cs
	namespace		 	
PTDuc		
 
.		 

WhereHouse		 
.		 
DL		 
.		 

Interfaces		 (
{

 
public 

	interface 
IDLBase 
< 
TEntity $
,$ %
TDTO% )
>) *
{ 
IEnumerable
<
TDTO
>
GetAll
(
)
;
TEntity 
GetByID 
( 
string 
Id !
)! "
;" #
bool 
Delete
( 
TEntity 
entity "
)" #
;# $
bool 
Update
( 
TEntity 
entity "
)" #
;# $
bool 
Insert
( 
TEntity 
entity "
)" #
;# $
IEnumerable 
< 
TEntity 
> 
GetByKey %
(% &
string& ,
key- 0
,0 1
string2 8
value9 >
)> ?
;? @
TEntity 
GetOneByKey 
( 
string "
key# &
,& '
string( .
value/ 4
)4 5
;5 6
	TableName 
GetOneByKeyWithType %
<% &
	TableName& /
>/ 0
(0 1
PropertyInfo1 =
prop> B
,B C
TEntityD K
entityL R
)R S
whereT Y
	TableNameZ c
:d e
classf k
;k l

GetByPaging !
(! "
int" %
page& *
,* +
int, /
pageSize0 8
)8 9
;9 :
} 
} �
qD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLConversation.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{ 
public 

	interface 
IDLConversation $
:% &
IDLBase' .
<. /
Conversation/ ;
,; <
ConversationDTO= L
>L M
{ 
} 
}		 �
iD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLFile.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{		 
public

 

	interface

 
IDLFile

 
:

 
IDLBase

 %
<

% &
File

& *
,

* +
FileDTO

+ 2
>

2 3
{ 
} 
}
jD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLHouse.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{ 
public 

	interface 
IDLHouse 
: 
IDLBase  '
<' (
House( -
,- .
HouseDTO/ 7
>7 8
{ 
IEnumerable		 
<		 
House		 
>		 
GetDeepData		 &
(		& '
)		' (
;		( )
}

 
} �
lD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLMessage.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{ 
public 

	interface 

IDLMessage 
:  !
IDLBase" )
<) *
Message* 1
,1 2

MessageDTO3 =
>= >
{ 
object		 
GetConversation		 
(		 

MessageDTO		 )
message		* 1
)		1 2
;		2 3
}

 
} �
iD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLPost.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{ 
public 

	interface 
IDLPost 
: 
IDLBase &
<& '
Post' +
,+ ,
PostDTO- 4
>4 5
{ 
} 
}		 �
iD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLUser.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{ 
public 

	interface 
IDLUser 
: 
IDLBase %
<% &
User& *
,* +
UserDTO, 3
>3 4
{ 
}		 
}

 �
mD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\IDLWishlist.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
{ 
public 

	interface 
IDLWishlist  
:! "
IDLBase# *
<* +
Wishlist+ 3
,3 4
WishlistDTO5 @
>@ A
{ 
} 
} �
pD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.DL\Interfaces\Login\IDLLogin.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
DL 
. 

Interfaces (
.( )
Login) .
{		 
public

 

	interface

 
IDLLogin

 
:

 
IDLBase

 %
<

% &

LoginParam

& 0
,

0 1

LoginParam

2 <
>

< =
{ 
LoginEntity 
Login 
( 
string  
Username! )
)) *
;* +
int
UpdatePasswordByEmployeeCode
(
string
employeecode
,
string
password
,
string
salt
)
;
} 
} 