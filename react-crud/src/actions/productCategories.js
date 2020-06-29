import api from "./api";

export const ACTION_TYPES = {   
    FETCH_ALL_CAT: 'FETCH_ALL_CAT'
}

export const fetchAll = () => dispatch => {
    api.productCategories().fetchAll()
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_CAT,
                payload: response.data                
            })
        })
        .catch(err => console.log(err))
}

