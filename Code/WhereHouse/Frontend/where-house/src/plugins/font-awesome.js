import Vue from 'vue';

/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'

/* import specific icons */
import { faUserSecret,faX,faPaperPlane,faInfo,faEllipsisVertical,faCirclePlus,faBan,faTrashCan,faRotate,faCheck ,faFlag} from '@fortawesome/free-solid-svg-icons'
import { faHeart,faMessage,faCircleUser,faFile, faEye } from '@fortawesome/free-regular-svg-icons'

/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

/* add icons to the library */
library.add(faUserSecret,faX,faHeart,faMessage,faPaperPlane,faInfo,faEllipsisVertical,faCircleUser,faFile,faCirclePlus,faBan,faTrashCan,faEye,faRotate,faCheck,faFlag)

/* add font awesome icon component */
Vue.component('font-awesome-icon', FontAwesomeIcon)