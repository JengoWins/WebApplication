import {PayloadAction, createSlice} from "@reduxjs/toolkit";

export interface CounterState{
	value: number;
}

const initialState: CounterState = {
  value: 0,
}

export const counterSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    increment: state => {
      state.value += 4
    },
    decrement: state => {
      state.value -= 4
    },
    incrementByAmount: (state, action: PayloadAction<number>) => {
      state.value += action.payload
    },
  },
});


export const { increment, decrement, incrementByAmount } = counterSlice.actions