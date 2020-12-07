import Vue from 'vue'

export default {
    state: {
        loanGamers: []
    },
    mutations: {
        setLoanGamers(state, loanGamers) {
            state.loanGamers = loanGamers
        }
    },
    actions: {
        getAPILoanGamers({ commit }) {
            return new Promise((resolve, reject) => {
                Vue.prototype.$http.get('/api/loanedgame').then(resp => {
                    const data = resp.data
                    //console.log(data);
                    if (data) {

                        commit('setLoanGamers', data)

                        resolve(data);
                    }
                }).catch(err => {
                    //console.log(err.response);

                    reject(err.response);
                })
            })
        },
        loanGameRegister({ commit }, loanGame) {
            return new Promise((resolve, reject) => {
                Vue.prototype.$http.post('/api/loanedgame', loanGame).then(resp => {
                    const data = resp.data.data
                    // eslint-disable-next-line
                    console.log(data);
                    
                    resolve(data);
                }).catch(err => {
                    // eslint-disable-next-line
                    //console.log(err);
                    //console.log('err');

                    reject(err.response);
                })
            })
        },
        loanGameUpdate({ commit }, loanGame) {
            return new Promise((resolve, reject) => {
                Vue.prototype.$http.put('/api/loanedgame', loanGame).then(resp => {
                    const data = resp.data.data
                    // eslint-disable-next-line
                    console.log(data);
                    
                    resolve(data);
                }).catch(err => {
                    // eslint-disable-next-line
                    //console.log(err);
                    //console.log('err');

                    reject(err.response);
                })
            })
        },
    },
    getters: {
        getLoanGamers: state => state.loanGamers
    }
}