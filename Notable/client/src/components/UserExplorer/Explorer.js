import React, { useState } from "react";
import { Button, Input, Modal, ModalBody, ModalHeader } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { _AddCategoryNote, _getAllNoteCategories, _getMyNotes, _getPublicNotes, _RemoveCategoryNote } from "../../modules/noteManager";
import { _getAllCategories } from "../../modules/categoryManager";

export default function Explorer(){
    const nav = useNavigate()
    const [notes, setNotes] = useState([])
    const [search, setSearch] = useState("")

    const [categories, setCategories] = useState([])
    const [categoryNoteId, setCategoryNoteId] = useState()
    const [showAddCategory, setShowAddCategory] = useState(false)

    //Update Categories
    const updateCategories = (noteId) =>{
        return _getAllCategories().then((categories) => {
            _getAllNoteCategories(noteId).then((noteCategories) => {
                categories.forEach(category => {
                    category.contains = (noteCategories.find((nc) => nc.id === category.id) !== undefined)
                });
                setCategories(categories)
            })
        })
    }

    const updateNotes = () => {
        _getPublicNotes(search).then((_notes) => {
            _notes = _notes.sort(function(a,b){
                return new Date(b.createdAt) - new Date(a.createdAt);
            });

            setNotes(_notes)
        })
    }

    useState(() => {
        updateNotes();
    })


    return (<>
         <div className="AddNoteButton">
         <Input onChange={(e) => {setSearch(e.target.value)}} style={{position:"absolute",left:"0px", width:"150px"}}></Input>
         <Button onClick={() => {updateNotes()}} style={{position:"absolute",left:"160px"}}>Search</Button>
         </div>
         <div style={{position:"absolute", zIndex:"-100",height:"300px"}} className="cleantable"></div>
         <table className="cleantable">
            <thead>
                <tr>
                    <td><b>Creator</b></td>
                    <td><b>Name</b></td>
                    <td><b>Date Added</b></td>
                    <td><b>Options</b></td>
                </tr>
            </thead>
            <tbody>
                {notes.map((note) => 
                (<tr key={note.id} className="dataRow">
                    <td>{note.userProfile.username}</td>
                    <td>{note.name}</td>
                    <td>{new Date(note.createdAt).toLocaleDateString()}</td>
                    <td>
                        <Button onClick={() => nav(`/notes/${note.id}`)} 
                        className="optionButton"><img src="/eye.svg" /></Button>

                        <Button onClick={() => {updateCategories(note.id).then(() => { setCategoryNoteId(note.id); setShowAddCategory(true)})}} 
                        className="optionButton"><img src="/folder.svg" /></Button>
                        {/*
                        <Button className="optionButton"><img src="/chat.svg" /></Button>
                        <ButtonToggle className="optionButton"><img src="/hand-thumbs-up.svg" /></ButtonToggle> 
                        */}
                    </td>
                </tr>)
                )}
            </tbody>
        </table>

        <Modal className="delModal" color="dark" isOpen={showAddCategory} toggle={() => setShowAddCategory(!showAddCategory)}>
          <ModalHeader className="darkmodal" toggle={() => setShowAddCategory(!showAddCategory)}>Add To Category</ModalHeader>
            <ModalBody className="darkmodal">
                {categories.map((category) => {
                    return (<div key={category.id}>
                        <Input checked={category.contains} onChange={(e) => {
                            if(e.target.checked)
                            {_AddCategoryNote(category.id, categoryNoteId).then(() => {updateCategories(categoryNoteId)})}
                            else{_RemoveCategoryNote(category.id, categoryNoteId).then(() => {updateCategories(categoryNoteId)})}
                        }} type="checkbox" id={category.id}/>
                        <label>{category.name}</label></div>)
                    })}
            </ModalBody>
        </Modal>
    </>)
}