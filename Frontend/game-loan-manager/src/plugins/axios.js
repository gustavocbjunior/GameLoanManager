import axios from 'axios'
import router from '../router'


var hostAPI;

//desenvolvimento
if (process.env.NODE_ENV !== "production") {
    hostAPI = "https://localhost:5001";
}
//produção
else {
    hostAPI = "http://localhost:8000";
}

axios.defaults.baseURL = hostAPI;
axios.defaults.headers["Access-Control-Allow-Origin"] = "*";
axios.defaults.headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS, HEAD";
axios.defaults.headers["Access-Control-Allow-Headers"] = "Access-Control-*, Origin, X-Requested-With, Content-Type, Accept";

var axiosInstance = axios.create({
    baseURL: hostAPI
})


// Request interceptor for API calls
axiosInstance.interceptors.request.use(
    async config => {
        const user = JSON.parse(localStorage.getItem('user'));
        if (user){
            config.headers = {
                'Authorization': `Bearer ${user.accessToken}`,
                'Accept': 'application/json',
                //'Content-Type': 'application/x-www-form-urlencoded'
            }
        } else {
            router.push('/login');
        }
        return config;
    },
    error => {
        Promise.reject(error)
    });

// Response interceptor for API calls
axiosInstance.interceptors.response.use((response) => {
    return response
}, async function (error) {
    const originalRequest = error.config;
    // eslint-disable-next-line
    console.log(error);
    if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;

        const user = JSON.parse(localStorage.getItem('user'));

        const access_token = user.accessToken;
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
        return axiosInstance(originalRequest);
    }
    return Promise.reject(error);
});

export default axiosInstance;
