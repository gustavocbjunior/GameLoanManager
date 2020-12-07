import Vue from 'vue'
import Router from 'vue-router'

import Home from './components/Home'
import NewUser from './components/user/NewUser'
import User from './components/user/User'
import Login from './components/user/Login'
import Game from './components/game/Game'
import LoanGame from './components/loanGame/LoanGame'

Vue.use(Router)

let router = new Router({
    mode: 'history',
    routes: [
        { path: '/', component: Home },
        { path: '/game', component: Game },
        { path: '/loanGame', component: LoanGame },
        { path: '/user/new', component: NewUser },
        { path: '/user', component: User },
        { path: '/login', component: Login },
    ]
})

router.beforeEach((to, from, next) => {
    const publicPages = ['/', '/login', '/user/new'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('user');
  
    // trying to access a restricted page + not logged in
    // redirect to login page
    if (authRequired && !loggedIn) {
      next('/login');
    } else {
      next();
    }
  });

export default router;