import { configureStore } from '@reduxjs/toolkit'
import {  counterSlice } from './counterSlice'
import { authSlice } from './Auth'


export const store = configureStore({
  reducer: {
	  counter: counterSlice.reducer,
    auth: authSlice.reducer,
    //Registration: RegSlice.reducer
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch