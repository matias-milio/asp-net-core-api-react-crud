import { ACTION_TYPES } from "../actions/productBrand";
const initialState = {
    list:[]
}

export const productBrand = (state = initialState,action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_BR:
            return {      
                ...state,          
                list: [...action.payload]
            }    
        default:
           return state;
    }
}