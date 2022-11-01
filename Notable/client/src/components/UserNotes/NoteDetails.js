import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Input, Label, Card, CardHeader, CardBody, CardFooter } from "reactstrap";
import { _addNote, _deleteNote, _getMyNotes, _getNoteById, _updateNote } from "../../modules/noteManager";
import "../styles/noteDetails.css"


export default function NoteDetails(){

    const {noteId} = useParams()
    const [note, setNote] = useState(null);
    const nav = useNavigate()
    const [newName, setNewName] = useState("")
    const [newContent, setNewContent] = useState("")
    const [newPublic, setNewPublic] = useState(false)
    const [canSave, setCanSave] = useState(false)
    const [deleteCounter, setDeleteCounter] = useState(3)

    const SaveNewNote = () =>{
        const newNote = {id:note.id,name:newName,content:newContent,isPublic:newPublic}
        _updateNote(noteId,newNote).then(() => {
            UpdateInfo().then(() => {
                setCanSave(false)
            })
        })
    }

    const UpdateInfo = () =>{
        return _getNoteById(noteId).then((n) => {
            setNote(n)
            setNewName(n?.name)
            setNewContent(n?.content)
            setNewPublic(n?.isPublic)
            setDeleteCounter(3)
        })
    }

    useEffect(() => {
        UpdateInfo()
    },[])


    return (note?.id === undefined) ? 
    (<><h3></h3></>) 
    : 
    (<>
        {(!note?.belongs) ? 
                (<>

                <Button style={{position:"absolute", left:"25%", top:"110px"}} onClick={() => nav(-1)}>Back</Button>
                <Card style={{borderColor:"darkgrey",borderStyle:"solid"}} className="mainCard" color="dark">
                        <section
                        style={{position:"absolute", marginLeft:"0%"}}
                        > Published: {new Date(note?.createdAt).toLocaleDateString()}</section>
                        <section
                        style={{position:"absolute", marginLeft:"0%", marginTop:"18px"}}
                        > By: {note?.userProfile?.username}</section>
                    <CardHeader>


                        {note?.name}
                    </CardHeader>
                    <CardBody>
                        {note?.content}
                    </CardBody>
                </Card>
                </>)
                :(<>
                <Button style={{position:"absolute", left:"25%", top:"110px"}} onClick={() => nav(-1)}>Back</Button>
                <Card style={{borderColor:"darkgrey",borderStyle:"solid"}} className="mainCard" color="dark">
                    <CardHeader>
                        <section
                        style={{position:"absolute", marginLeft:"0%"}}
                        > Published: {new Date(note?.createdAt).toLocaleDateString()}</section>
                        <input 
                        maxLength="15" 
                        className="editNameTB" 
                        defaultValue={note?.name}
                        onChange={(e) => {setCanSave(true); setNewName(e.target.value)}}></input>
                    </CardHeader>
                    <CardBody>
                        <Label>Content:</Label>
                        <Input
                        maxLength="999"
                        className="editContentTB" 
                        type="textarea" 
                        defaultValue={note?.content}
                        onChange={(e) => {setCanSave(true); setNewContent(e.target.value)}}></Input>
                        <Label>Is Public <Input onChange={(e) => {setCanSave(true); setNewPublic(e.target.checked)}} defaultChecked={note?.isPublic} type="checkbox"></Input></Label>
                    </CardBody>
                    <CardFooter>
                        <Button onClick={() => {SaveNewNote();nav(-1)}} disabled={!canSave} className="optionButton" color="primary">Save</Button>
                        <Button className="optionButton" onClick={() => {
                            if(deleteCounter > 1){
                                setDeleteCounter(deleteCounter - 1)
                            }else{
                                if(_deleteNote(note?.id)){
                                    nav("/")
                                }
                            }
                        }} color="danger">Delete ({deleteCounter})</Button>
                    </CardFooter>
                </Card>
            </>)}
    </>);
}
    