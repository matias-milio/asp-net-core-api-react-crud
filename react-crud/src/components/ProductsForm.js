import React, { useEffect } from "react";
import { Grid, TextField, withStyles, FormControl, InputLabel, Select, MenuItem, Button, FormHelperText } from "@material-ui/core";
import useForm from "./useForm";
import { connect } from "react-redux";
import * as productActions from "../actions/product";
import * as productBrandActions from "../actions/productBrand";
import * as productCategoryActions from "../actions/productCategories";
import { useToasts } from "react-toast-notifications";

const styles = theme => ({
    root: {                
        '& .MuiTextField-root': {
            margin: theme.spacing(1),
            minWidth: 230,
        }
    },
    palette: {
        background: {
          default: "#303030"
        }
      },   
    formControl: {
        margin: theme.spacing(1),
        minWidth: 230,
    },
    smMargin: {
        margin: theme.spacing(2.5)
    },
    numberInput: {
        margin: theme.spacing(1.3),
        width:'26ch'
    }
})

const initialFieldValues = {
    name: '',
    price: '',
    stock: '',
    ProductCategoryId: '',
    ProductBrandId: ''    
}

const ProductsForm = ({ classes, ...props }) => {

    const { addToast } = useToasts()
    const validate = (fieldValues = values) => {
        let temp = { ...errors }
        if ('name' in fieldValues)
            temp.name = fieldValues.name ? "" : "This field is required."
        if ('price' in fieldValues)
            temp.price = fieldValues.price ? "" : "This field is required."
        if ('price' in fieldValues)
             temp.price = fieldValues.price > 0 ? "" : "This field cant be less than 1 (one)."
        if ('stock' in fieldValues)
            temp.stock = fieldValues.stock ? "" : "This field is required."
        if ('stock' in fieldValues)
            temp.stock = fieldValues.stock > 0 ? "" : "This field cant be less than 1 (one)."
        if ('ProductCategoryId' in fieldValues)
            temp.ProductCategoryId = fieldValues.ProductCategoryId ? "" : "This field is required."
        if ('ProductBrandId' in fieldValues)
            temp.ProductBrandId = fieldValues.ProductBrandId ? "" : "This field is required."
        
        setErrors({
            ...temp
        })

        if (fieldValues == values)
            return Object.values(temp).every(x => x == "")
    }

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm,
        NumberFormatCustom
    } = useForm(initialFieldValues, validate, props.setCurrentId)
        
    //material-ui select
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = React.useState(0);
    React.useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth);
    }, []);

    const handleSubmit = e => {        
        e.preventDefault()
        if (validate()) {
            const onSuccess = () => {
                addToast("Product added !", { appearance: 'success' })
                window.location.reload()
                resetForm()                
            }
            if (props.currentId == 0)
                props.createProduct(values, onSuccess)
            else
                props.updateProduct(props.currentId, values, onSuccess)
        }

    }

    useEffect(() => {    
        props.fetchCategories()
        props.fetchBrands()        
        if (props.currentId != 0) {
            setValues({
                ...props.dProductList.find(x => x.id == props.currentId)
            })
            setErrors({})
        }
    }, [props.currentId])

    return (
        <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
            <Grid container>
                <Grid item xs={6}>
                    <TextField
                        name="name"
                        variant="outlined"
                        label="Product name"
                        value={values.name}
                        onChange={handleInputChange}
                        {...(errors.name && { error: true, helperText: errors.name })}
                    />
                    
                        <TextField
                            id="outlined-adornment-amount"
                            name="price"
                            type="number"
                            variant="outlined"
                            label="Product price"
                            value={values.price}  
                            InputProps={{
                            inputComponent: NumberFormatCustom,
                            }}
                            onChange={handleInputChange}
                            {...(errors.price && { error: true, helperText: errors.price })}
                        />                    

                        <TextField
                        name="stock"
                        variant="outlined"
                        label="Product current stock"
                        type="number"
                        value={values.stock}
                        onChange={handleInputChange}
                        {...(errors.stock && { error: true, helperText: errors.stock })}
                    />
                   
                </Grid>
                <Grid item xs={6}>  

                    <FormControl variant="outlined"
                        className={classes.formControl}
                        {...(errors.ProductCategoryId && { error: true })}
                    >
                        <InputLabel ref={inputLabel}>Available Categories</InputLabel>
                        <Select
                            name="ProductCategoryId"
                            value={values.ProductCategoryId}
                            onChange={handleInputChange}
                            labelWidth={labelWidth}                            
                        >       
                         {props.dProductCategories.map(categoria => (
                            <MenuItem 
                                key={categoria.id} 
                                value={categoria.id} 
                            >{categoria.description}</MenuItem>
                        ))}                     
                        </Select>
                        {errors.ProductCategoryId && <FormHelperText>{errors.ProductCategoryId}</FormHelperText>}
                    </FormControl>
                   
                   <FormControl variant="outlined"
                        className={classes.formControl}
                        {...(errors.ProductBrandId && { error: true })}
                    >
                        <InputLabel ref={inputLabel}>Available Brands</InputLabel>
                        <Select
                            name="ProductBrandId"
                            value={values.ProductBrandId}
                            onChange={handleInputChange}
                            labelWidth={labelWidth}                            
                        >
                          {props.dProductBrands.map(marca => (
                            <MenuItem 
                                key={marca.id} 
                                value={marca.id} 
                            >{marca.description}</MenuItem>
                        ))}      
                        </Select>
                        {errors.ProductBrandId && <FormHelperText>{errors.ProductBrandId}</FormHelperText>}
                    </FormControl>

                    <div>
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            className={classes.smMargin}
                        >
                            Create
                        </Button>
                        <Button
                            variant="contained"
                            className={classes.smMargin}
                            onClick={resetForm}
                        >
                            Reset
                        </Button>
                    </div>
                </Grid>
            </Grid>
        </form>
    );
}


const mapStateToProps = state => ({
    dProductList: state.product.list,
    dProductCategories: state.productCategories.list,
    dProductBrands: state.productBrand.list
})

const mapActionToProps = {
    createProduct: productActions.create,
    updateProduct: productActions.update,
    fetchCategories: productCategoryActions.fetchAll,
    fetchBrands: productBrandActions.fetchAll
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(ProductsForm));