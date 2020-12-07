import Vue from 'vue'
import axios from 'axios'

Vue.use({
    install(Vue) {
        var hostAPI = 'https://localhost:5001';
        
        axios.defaults.baseURL = hostAPI;
        axios.defaults.headers["Access-Control-Allow-Origin"] = "*";
        axios.defaults.headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS, HEAD";
        axios.defaults.headers["Access-Control-Allow-Headers"] = "Access-Control-*, Origin, X-Requested-With, Content-Type, Accept";

        Vue.prototype.$http = axios.create({
            baseURL: hostAPI,
            /*headers: {
                Autorization: 'Bearer' + localStorage.getItem('token') || ''
            }*/
        })

        const user = JSON.parse(localStorage.getItem('user'));
        if(user) {
            Vue.prototype.$http.defaults.headers.common['Authorization'] = 'Bearer ' + user.accessToken
        }
    }
})