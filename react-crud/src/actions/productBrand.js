import api from "./api";

export const ACTION_TYPES = {    
    FETCH_ALL_BR: 'FETCH_ALL_BR'
}

export const fetchAll = () => dispatch => {
    api.productBrands().fetchAll()
        .then(response => {            
            dispatch({
                type: ACTION_TYPES.FETCH_ALL_BR,
                payload: response.data
            })
        })
        .catch(err => console.log(err))
}


