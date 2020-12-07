import Vue from 'vue'
import router from '../../router'

export default {
    state: {
        user: JSON.parse(localStorage.getItem('user')),
        users: []
    },
    mutations: {
        setUser(state, user) {
            //console.log("chamou setUser");
            state.user = user
        },
        setUsers(state, users) {
            //console.log("chamou setUser");
            state.users = users
        }
    },
    actions: {
        authentication({ commit }, auth) {
            return new Promise((resolve, reject) => {
                Vue.prototype.$http.post('/api/User/Authentication', auth).then(resp => {
                    const data = resp.data.data
                    //console.log(data);
                    if (data.accessToken) {
                        var responseJson = JSON.stringify(data)
                        localStorage.setItem('user', responseJson);

                        commit('setUser', data)

                        resolve(data);
                    }
                }).catch(err => {
                    //console.log(err);
                    localStorage.removeItem('token')

                    reject(err);
                })
            })
        },
        logout({ commit }) {
            //console.log("chamou");
            commit('setUser', '')
            localStorage.removeItem('user');
            router.push('/login');
        },
        getUsers({ commit }) {
            return new Promise((resolve, reject) => {
                Vue.prototype.$http.get('/api/User').then(resp => {
                    const data = resp.data
                    //console.log(data);
                    if (data) {
                        commit('setUsers', data)

                        resolve(data);
                    }
                }).catch(err => {
                    //console.log(err);
                    localStorage.removeItem('token')

                    reject(err);
                })
            })
        },
        userRegister({ commit }, user) {
            return new Promise((resolve, reject) => {
                Vue.prototype.$http.post('/api/User', user).then(resp => {
                    const data = resp.data
                    console.log(data);
                    if (data) {
                        resolve(data);
                    }
                }).catch(err => {
                    console.log(err);

                    reject(err.response);
                })
            })
        },
    },
    getters: {
        isLoggedIn: state => !!state.user,
        loggedInUser: state => state.user
    }
}