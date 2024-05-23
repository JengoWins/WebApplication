import {createSlice} from "@reduxjs/toolkit";

export interface CounterState{
  access_token: string,
  date: string,
  username: string
}

export interface Token{
    access_token: string,
    date: string,
    username: string
}

const initialState: CounterState = {
  access_token: "",
  date: "",
  username:""
}
var tokenKey = "accessToken";

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    login: state=>{
      const data = sessionStorage.getItem(tokenKey)!;
      let obj: Token = JSON.parse(data);
      state.username = obj.username;
      state.date = obj.date;
      state.access_token = obj.access_token;
  }
}
});

export const { login } = authSlice.actions