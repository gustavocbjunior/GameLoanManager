import axiosInstance from '../../plugins/axios'

export default {
    state: {
        gamers: []
    },
    mutations: {
        setGamers(state, gamers) {
            state.gamers = gamers
        }
    },
    actions: {
        getAPIGamers({ commit }) {
            return new Promise((resolve, reject) => {
                axiosInstance.get('/api/Game').then(resp => {
                    const data = resp.data
                    //console.log(data);
                    if (data) {

                        commit('setGamers', data)

                        resolve(data);
                    }
                }).catch(err => {
                    //console.log(err.response);

                    reject(err.response);
                })
            })
        },
        gameRegister({ commit }, game) {
            return new Promise((resolve, reject) => {
                axiosInstance.$http.post('/api/Game', game).then(resp => {
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
        gameUpdate({ commit }, game) {
            return new Promise((resolve, reject) => {
                axiosInstance.$http.put('/api/Game', game).then(resp => {
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
        getGamers: state => state.gamers
    }
}