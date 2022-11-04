import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Input, Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import { _addCategory, _deleteCategory, _getAllCategories } from "../../modules/categoryManager";
import { _getAllCategoryNotes, _RemoveCategoryNote } from "../../modules/noteManager";

export default function CategoryList(){
    const nav = useNavigate()
    const [categories, setCategories] = useState([])
    const [showCreateModal, setShowCreateModal] = useState(false)
    const [newCategoryName, setNewCategoryName] = useState("")

    const [category, setCategory] = useState({})
    const [categoryNotes, setCategoryNotes] = useState([])
    const [showViewModal, setShowViewModal] = useState(false)



    const UpdateCategories = () => {
        _getAllCategories().then((categories) => {
            setCategories(categories)
        })
    }

    const UpdateCategoryNotes = () => {
        if(category?.id !== undefined){
            _getAllCategoryNotes(category?.id).then((categories) => {
                setCategoryNotes(categories)
            })
        }
    }

    useEffect(() =>{
        UpdateCategories();
    }, [])


    useEffect(() => {
        UpdateCategoryNotes()
    },[category])

return (<>

    <div className="AddNoteButton">
        <Button style={{marginRight:"10px"}} onClick={() => nav(-1)}>Back</Button>
        <Button onClick={() => setShowCreateModal(!showCreateModal)}>Create a Category</Button>
    </div>
    <div style={{position:"absolute", zIndex:"-100",height:"300px"}} className="cleantable"></div>
    <table className="cleantable">
        <thead>
            <tr>
                <td style={{width:"500px"}}>Category</td>
                <td>Options</td>
            </tr>
        </thead>
        <tbody>
            {categories?.map((category) => (<tr key={category.id}>
                    <td>{category.name}</td>
                    <td>
                        <Button onClick={() => {setCategory(category); setShowViewModal(true)}} style={{marginRight:"10px"}}>View Notes</Button>
                        <Button color="danger" onClick={() => {_deleteCategory(category.id).then(() => {UpdateCategories()})}}>Delete</Button>
                    </td>
                </tr>))}
        </tbody>
    </table>
    <Modal className="delModal" color="dark" isOpen={showCreateModal} toggle={() => setShowCreateModal(!showCreateModal)}>
        <ModalHeader className="darkmodal" toggle={() => setShowCreateModal(!showCreateModal)}>Create a Category</ModalHeader>
        <ModalBody className="darkmodal">
            <label>Category Name:</label>
            <Input onChange={(e) =>  {setNewCategoryName(e.target.value);}} style={{backgroundColor:"#454545",borderColor:"#454545", color:"white"}}></Input>
        </ModalBody>
        <ModalFooter className="darkmodal">
            <Button color="primary" onClick={() => _addCategory(newCategoryName).then(() => { UpdateCategories(); setShowCreateModal(!showCreateModal)})}>Create</Button>
            <Button color="secondary" onClick={() => setShowCreateModal(!showCreateModal)}>Cancel</Button>
        </ModalFooter>
    </Modal>
    <Modal className="delModal" color="dark" isOpen={showViewModal} toggle={() => setShowViewModal(!showViewModal)}>
        <ModalHeader className="darkmodal" toggle={() => setShowViewModal(!showViewModal)}>Notes from "{category?.name}"</ModalHeader>
        <ModalBody className="darkmodal">

        <table style={{width:"100%", textAlign:"center"}}>
            <tbody>
                <tr>
                    <td>Note</td>
                    <td>Options</td>
                </tr>
                {categoryNotes?.map((note) => (<>
                    <tr>
                        <td>
                            {note.name}
                        </td>
                        <td>
                        <Button 
                        onClick={() => nav(`/notes/${note.id}`)} 
                        className="optionButton"><img src="/eye.svg" /></Button>
                            <Button onClick={() => _RemoveCategoryNote(category.id, note.id).then(() => UpdateCategoryNotes())} 
                            color="danger">Remove</Button>
                        </td>
                    </tr>
                </>))}
            </tbody>
        </table>
        </ModalBody>
    </Modal>
</>)
}