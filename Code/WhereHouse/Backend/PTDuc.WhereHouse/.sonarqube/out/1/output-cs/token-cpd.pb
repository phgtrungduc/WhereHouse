?
zD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Attribute\CustomAttribute.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
	Attribute( 1
{ 
[		 
AttributeUsage		 
(		 
AttributeTargets		 $
.		$ %
Property		% -
)		- .
]		. /
public

 

class

 
Required

 
:

 
FlagsAttribute

 *
{ 
} 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public 

class 
CheckDuplicate 
:  !
FlagsAttribute" 0
{ 
} 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public 

class 

PrimaryKey 
: 
FlagsAttribute ,
{ 
} 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Property% -
)- .
]. /
public   

class   
Email   
:   
FlagsAttribute   '
{!! 
}## 
['' 
AttributeUsage'' 
('' 
AttributeTargets'' $
.''$ %
Property''% -
)''- .
]''. /
public(( 

class(( 
	MaxLength(( 
:(( 
FlagsAttribute(( +
{)) 
public** 
int** 
Value** 
{** 
get** 
;** 
set**  #
;**# $
}**% &
public++ 
string++ 
ErrorMsg++ 
{++  
get++! $
;++$ %
set++& )
;++) *
}+++ ,
public,, 
	MaxLength,, 
(,, 
int,, 
length,, #
,,,# $
string,,% +
errorMsg,,, 4
),,4 5
{-- 	
this.. 
... 
Value.. 
=.. 
length.. 
;..  
this// 
.// 
ErrorMsg// 
=// 
errorMsg// $
;//$ %
}00 	
}11 
[55 
AttributeUsage55 
(55 
AttributeTargets55 $
.55$ %
Property55% -
)55- .
]55. /
public66 

class66  
DisplayNameAttribute66 %
:66& '
FlagsAttribute66( 6
{667 8
}77 
}88 ?
tD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\ConversationDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public		 

partial		 
class		 
ConversationDTO		 (
{

 
public 
Guid 
ConversationId "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
Guid 
UserId1 
{ 
get !
;! "
set# &
;& '
}( )
public 
Guid 
UserId2 
{ 
get !
;! "
set# &
;& '
}( )
public 
virtual 
ICollection "
<" #
Message# *
>* +
Messages, 4
{5 6
get7 :
;: ;
set< ?
;? @
}A B
} 
} ?	
lD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\FileDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public 

class 
FileDTO 
{		 
public

 
Guid

 
FileId

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 
string 
FileName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
FilePath 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
	CreatedBy 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
? 
CreatedDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ?
mD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\HouseDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public 

class 
HouseDTO 
{		 
public 
Guid 
HouseId 
{ 
get !
;! "
set# &
;& '
}( )
public 
Guid 
UserOwnerId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
	HouseName 
{  !
get" %
;% &
set' *
;* +
}, -
public 
Guid 
? 
HouseTypeId  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
float 
? 
Area 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
NumberOfBedroom "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
int 
? 
TotalOfFloor  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
float 
? 

Horizontal  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
float 
? 
Vertical 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal 
? 
Price 
{ 
get  #
;# $
set% (
;( )
}* +
public 
Guid 
? 
HouseImageId !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
DateTime 
? 
CreatedDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
	CreatedBy 
{  !
get" %
;% &
set' *
;* +
}, -
public 
bool 
? 
IsInWishList !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ?
qD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\HouseTypeDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public 

class 
HouseTypeDTO 
{		 
public

 
Guid

 
HouseTypeId

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
string 
HouseTypeName #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} ?
oD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\MessageDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public		 

partial		 
class		 

MessageDTO		 #
{

 
public 
Guid 
	MessageId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
Guid 
? 
ConversationId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
Time 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Content 
{ 
get  #
;# $
set% (
;( )
}* +
public 
virtual 
Conversation #
Conversation$ 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
virtual 
User 
User  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} ?
lD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\PostDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public		 

class		 
PostDTO		 
{

 
public 
Guid 
PostId 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Descrtiption "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
HouseId 
{ 
get !
;! "
set# &
;& '
}( )
public 
virtual 
House 
House "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
virtual 
User 
User  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} ?
lD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\UserDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public		 

class		 
UserDTO		 
{

 
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
PhoneNumber !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
FullName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
ProvinceCode "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
DistrictCode "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
WardCode 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Salt 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
? 
AvatarId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
Role 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
Status 
{ 
get 
;  
set! $
;$ %
}& '
public 
DateTime 
? 
CreatedDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
	CreatedBy 
{  !
get" %
;% &
set' *
;* +
}, -
public 
virtual 
File 
Avatar "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} ?	
pD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\DTO\WishlistDTO.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
DTO( +
{ 
public 

class 
WishlistDTO 
{		 
public

 
Guid

 

WishlistId

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
PostId 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
? 
CreatedDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
DateTime 
? 
CreatedTime $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ?

xD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Enumeration\Enumeration.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
{ 
public		 

class		 
Enumeration		 
{

 
public 
enum 

ResultCode 
{ 	
Failed 
= 
$num 
, 
Success 
= 
$num 
, 
Authenticated 
= 
$num 
,  
UserNotFound 
= 
$num 
, 
PasswordNotCorrect 
=  
$num! $
,$ %
NotTrueParam 
= 
$num 
, 
NotExistFile 
= 
$num 
,  
NotExistFolder 
= 
$num  
} 	
public 
enum 
	HouseType 
{ 	
Street 
= 
$num 
, 
Alley 
= 
$num 
, 
Villa 
= 
$num 
, 
AdjoiningStreet 
= 
$num 
} 	
} 
} ?
xD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\Address\District.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
Models( .
{		 
public

 

class

 
District

 
{ 
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Slug 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str &
)& '
]' (
public 
string 
NameWithType "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Path 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str &
)& '
]' (
public 
string 
PathWithType "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Code 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str #
)# $
]$ %
public 
string 

ParentCode  
{! "
get# &
;& '
set( +
;+ ,
}- .
}   
}!! ?
xD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\Address\Province.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
Models( .
{		 
public

 

class

 
Province

 
{ 
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Slug 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str &
)& '
]' (
public 
string 
NameWithType "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Code 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ?
tD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\Address\Ward.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
Models( .
{		 
public

 

class

 
Ward

 
{ 
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Slug 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str &
)& '
]' (
public 
string 
NameWithType "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Path 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str &
)& '
]' (
public 
string 
PathWithType "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Code 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str #
)# $
]$ %
public 
string 

ParentCode  
{! "
get# &
;& '
set( +
;+ ,
}- .
}   
}!! ?
rD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\BaseEntity.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
{ 
public 

class 

BaseEntity 
{ 
public 
DateTime 
? 
CreatedDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
string 
	CreatedBy 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
? 
ModifiedDate %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 

ModifiedBy  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} ?	
rD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\FileUpload.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
.' (
Models( .
{		 
public

 

class

 

FileUpload

 
{ 
public 
Guid 
? 
FileId 
{ 
get !
;! "
set# &
;& '
}( )
public 
	IFormFile 
file 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
category 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
fileName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
filePath 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ?

yD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\Login\LoginEntity.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
{ 
public 

class 
LoginEntity 
: 

BaseEntity )
{ 
[ 	
Required	 
] 
[		 	
System			 
.		 
ComponentModel		 
.		 
DisplayName		 *
(		* +
$str		+ :
)		: ;
]		; <
public

 
string

 
Username

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
[ 	
Required	 
] 
[ 	
System	 
. 
ComponentModel 
. 
DisplayName *
(* +
$str+ 5
)5 6
]6 7
public 
byte 
[ 
] 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
byte 
[ 
] 
Salt 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ?
xD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\Login\LoginParam.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
{		 
public

 

class

 

LoginParam

 
{ 
[ 	
Required	 
] 
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
] 
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Salt 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
HashPassword "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} ?
uD:\Tailieu\projectDoAn\Code\WhereHouse\Backend\PTDuc.WhereHouse\PTDuc.WhereHouse.EntityModels\Models\ServiceResult.cs
	namespace 	
PTDuc
 
. 

WhereHouse 
. 
EntityModels '
{ 
public 

class 
ServiceResult 
{		 
public

 
ServiceResult

 
(

 
)

 
{ 	

StatusCode 
= 
( 
int 
) 
Enumeration )
.) *

ResultCode* 4
.4 5
Success5 <
;< =
} 	
public 
object 
Data 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
	Messenger 
{  !
get" %
;% &
set' *
;* +
}, -
public 
int 

StatusCode 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} 