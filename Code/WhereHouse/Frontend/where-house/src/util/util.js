import swal from "sweetalert";
const util = {
    validateEmail(email) {
        return String(email)
            .toLowerCase()
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    },
    notifyRequiredLogin(router) {
        swal({
            title: "Chức năng yêu cầu đăng nhập",
            text: "Xác nhận chuyển hướng đến trang đăng nhập?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        }).then((isRedirect) => {
            if (isRedirect) {
                // swal("Bạn sẽ được chuyển hướng đến trang đăng nhập.", {
                //   icon: "success",
                // });
                router.push({ name: 'Signin' })
            }
        });
    },
    setCookie(c_name, value, exdays) {
        let exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        let c_value = encodeURI(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    },
    
    getCookie(c_name) {
        let i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substring(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substring(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return decodeURI(y);
            }
        }
    },
    getUserConfig(){
        let config = this.getCookie("userConfig");
        if (config) return JSON.parse(config);
        return {};
    },
    getCurrentUserId(){
        let user = this.getUserConfig();
        return user?user.UserId:'';
    }

}
export default util;