import axios from "axios";

const baseUrl = "http://localhost:53808/api/"

export default {
    product(url = baseUrl + 'products'){
        return {
            fetchAll: () => axios.get(url),
            fetchById: id=> axios.get(url+ "/" +id),
            create: newRecord => axios.post(url,newRecord),
            update: (id,updateRecord) => axios.put(url+ "/" +id,updateRecord),
            delete: id=>axios.delete(url + "/" + id)
        }
    },
    productBrands(url = baseUrl + 'productBrands'){
        return {
            fetchAll: () => axios.get(url),
            fetchById: id=> axios.get(url+ "/" +id)
        }
    },
    productCategories(url = baseUrl + 'productCategories'){
        return {
            fetchAll: () => axios.get(url),
            fetchById: id=> axios.get(url+ "/" +id)
        }
    }
}