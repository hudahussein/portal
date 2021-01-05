import Vue from 'vue'
import Router from 'vue-router'
import login from '@/components/login'
import home from '@/components/home'
import workRequest from '@/components/Hr_portal/workRequest/workRequest'
import vacationRequest from '@/components/Hr_portal/vacationRequest/vacationRequest'

Vue.use(Router)

const router = new Router({
  // mode:'history',
  routes: [
    {
      path:'/',
      name:'login',
      component:login
    },    
    {
      path:'/home',
      name:'home',
      component:home,
      meta: {
        requiresAuth:false
      }
    },
    {
      path: '/work-request',
      name: 'workRequest',
      component: workRequest,
      meta: {
        requiresAuth:false
      }
    },
    {
      path: '/vacation-request',
      name: 'vacationRequest',
      component: vacationRequest,
      meta: {
        requiresAuth:false
      }
    }
  ]
});

router.beforeEach((to,from,next) => {
  // check to see if route requires auth
  if (to.matched.some(rec => rec.meta.requiresAuth)) {
    // check auth state of user
    let user = localStorage.getItem('user');
    if (user) {
      // user signed in, proceed to route
      next()
    } else {
      // no user signed in, redirect to login
      next({name:'login'})
    }
  } else {
    next()
  }
});

export default router
