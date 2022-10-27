import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, ButtonToggle, Form, Input, Label, Modal, ModalHeader, ModalBody, ModalFooter, Card, CardHeader, CardBody, CardFooter, Accordion, AccordionItem, AccordionBody, Collapse, AccordionHeader, Col } from "reactstrap";
import { _getAllCategories } from "../../modules/categoryManager";

export default function CategoryList(){
    const navigate = useNavigate()
    const [categories, setCategories] = useState([])


    useEffect(() =>{
        _getAllCategories().then((categories) => {
            setCategories(categories)
        })
    })

return (<>

    <div className="tableBackButton">
        <Button style={{marginRight:"10px"}} onClick={() => navigate(-1)}>Back</Button>
        <Button>Create a Category</Button>
    </div>
    <table>
        <thead>
            <tr>
                <td style={{width:"500px"}}>Category</td>
                <td>Options</td>
            </tr>
        </thead>
        <tbody>
            {categories?.map((category) => (<>
                <tr>
                <td>{category.name}</td>
                <td>
                    <Button style={{marginRight:"10px"}}>View Notes</Button>
                    <Button>Delete</Button>
                </td>
            </tr>
                </>))}
        </tbody>
    </table>

</>)
}