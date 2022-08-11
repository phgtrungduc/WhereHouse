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
    getUserConfig() {
        let config = this.getCookie("userConfig");
        if (config) return JSON.parse(config);
        return {};
    },
    getCurrentUserId() {
        let user = this.getUserConfig();
        return user ? user.UserId : '';
    },
    checkObjectHasData(obj) {
        let res = false;
        if (!obj) {
            res = false;
        } else {
            let arrayValue = Object.values(obj);
            if (arrayValue && arrayValue.length > 0) {
                arrayValue.forEach(x => {
                    if (x) res = true;
                })
            } else {
                res = false;
            }
        }
        return res;
    },
    alertSuccess(title) {
        return swal(title, {
            buttons: false,
            timer: 1000,
            icon: "success",
        })
    },
    checkLogin() {
        let userId = this.getCurrentUserId();
        let token = localStorage.getItem("token");
        if (userId && token) return true;
        return false;
    },
    objectEquals(x, y) {
        if (x === y) return true;
        // if both x and y are null or undefined and exactly the same

        if (!(x instanceof Object) || !(y instanceof Object)) return false;
        // if they are not strictly equal, they both need to be Objects

        if (x.constructor !== y.constructor) return false;
        // they must have the exact same prototype chain, the closest we can do is
        // test there constructor.

        for (var p in x) {
            if (!Object.prototype.hasOwnProperty.call(x,p)) continue;
            // other properties were tested using x.constructor === y.constructor

            if (!Object.prototype.hasOwnProperty.call(y,p)) return false;
            // allows to compare x[ p ] and y[ p ] when set to undefined

            if (x[p] === y[p]) continue;
            // if they have the same strict value or identity then they are equal

            if (typeof (x[p]) !== "object") return false;
            // Numbers, Strings, Functions, Booleans must be strictly equal

            if (!this.objectEquals(x[p], y[p])) return false;
            // Objects and Arrays must be tested recursively
        }

        for (p in y)
            if (Object.prototype.hasOwnProperty.call(y,p) && !Object.prototype.hasOwnProperty.call(x,p))
                return false;
        // allows x[ p ] to be set to undefined

        return true;
    }

}
export default util;