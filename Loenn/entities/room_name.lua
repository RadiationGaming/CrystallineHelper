local roomName = {}

roomName.name = "vitellary/roomname"
roomName.depth = -100

roomName.placements = {
    {
        name = "room_name",
        data = {
            roomName = "",
            backgroundColor = "000000FF",
            textColor = "FFFFFFFF",
            disappearTimer = -1,
        }
    }
}
roomName.fieldInformation = {
    backgroundColor = {
        fieldType = "color"
    },
    textColor = {
        fieldType = "color"
    },
}

roomName.texture = "ahorn_roomname"

return roomName
