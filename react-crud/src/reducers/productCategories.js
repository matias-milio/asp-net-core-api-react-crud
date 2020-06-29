import { ACTION_TYPES } from "../actions/productCategories";
const initialState = {
    list:[]
}

export const productCategories = (state = initialState,action) => {
    switch (action.type) {
        case ACTION_TYPES.FETCH_ALL_CAT:
            return {      
                ...state,          
                list: [...action.payload]
            }    
        default:
           return state;
    }
}