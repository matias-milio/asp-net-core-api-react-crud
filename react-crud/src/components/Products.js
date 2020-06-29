import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import * as actions from "../actions/product";
import { Grid, Paper, TableContainer, Table, TableHead, TableRow, TableCell, TableBody, withStyles, ButtonGroup, Button } from "@material-ui/core";
import ProductsForm from "./ProductsForm";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { useToasts } from "react-toast-notifications";


const styles = theme => ({
    root: {        
        "& .MuiTableCell-head": {
            fontSize: "1.25rem"
        }
    },
    paper: {
        background: 'linear-gradient(45deg, #E8E8E8 30%, #BEBEBE 90%)',
        margin: theme.spacing(2),
        padding: theme.spacing(2)
    }
})

const Products = ({ classes, ...props }) => {
    const [currentId, setCurrentId] = useState(0)

    useEffect(() => {
        props.fetchAllProducts()
    }, [])   
    const { addToast } = useToasts()

    const onDelete = id => {
        if (window.confirm('Are you sure to delete this product?'))
            props.deleteProduct(id,()=>addToast("Deleted successfully", { appearance: 'info' }))
            window.location.reload()
        }
    return (
        <Paper className={classes.paper} elevation={3}>
            <Grid container>
                <Grid item xs={6}>
                    <ProductsForm {...({ currentId, setCurrentId })} />
                </Grid>
                <Grid item xs={6}>
                    <TableContainer>
                        <Table>
                            <TableHead className={classes.root}>
                                <TableRow>
                                    <TableCell>Product name</TableCell>
                                    <TableCell>Product price</TableCell>
                                    <TableCell>Product current stock</TableCell>
                                    <TableCell></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    props.dProductList.map((record, index) => {
                                        return (<TableRow key={index} hover>
                                            <TableCell>{record.name}</TableCell>
                                            <TableCell>{record.price}</TableCell>
                                            <TableCell>{record.stock}</TableCell>
                                            <TableCell>
                                                <ButtonGroup variant="text">
                                                    <Button><EditIcon color="primary"
                                                        onClick={() => { setCurrentId(record.id) }} /></Button>
                                                    <Button><DeleteIcon color="secondary"
                                                        onClick={() => onDelete(record.id)} /></Button>
                                                </ButtonGroup>
                                            </TableCell>
                                        </TableRow>)
                                    })
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
            </Grid>
        </Paper>
    );
}

const mapStateToProps = state => ({
    dProductList: state.product.list
})

const mapActionToProps = {
    fetchAllProducts: actions.fetchAll,
    deleteProduct: actions.Delete
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(Products));