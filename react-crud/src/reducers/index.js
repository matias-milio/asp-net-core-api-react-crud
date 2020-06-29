import { combineReducers } from "redux";
import { product } from "./product"
import { productCategories } from "./productCategories"
import { productBrand } from "./productBrand"

export const reducers = combineReducers(
    {
        product,
        productCategories,
        productBrand
    }
)