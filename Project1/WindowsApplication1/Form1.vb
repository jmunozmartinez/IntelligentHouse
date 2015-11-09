Public Class Form1

    Dim lockStatus As String = "Open"
    Dim currentTemp As Integer = 23
    Dim roomTemp As Integer = 17
    Dim kitchenTemp As Integer = 16
    Dim garageTemp As Integer = 8
    Dim corridorTemp As Integer = 18
    Dim bathroomTemp As Integer = 22
    Dim livingTemp As Integer = 20
    Dim orangeColor As Color = Color.DarkOrange
    Dim greenColor As Color = Color.YellowGreen
    Dim darkColor As Color = Color.CornflowerBlue
    Dim blueColor As Color = Color.DeepSkyBlue
    Dim redColor As Color = Color.Red
    'Constant for the Energy Bar
    Dim EnergyValue As Integer = 0
    'Dim OpenLock As String = "E:\Visual Studio 2012\Projects\LOCKopenSMALL.jpg"
    'Dim ClosedLock As String = "E:\Visual Studio 2012\Projects\LOCKclosedSMALL.jpg"
    Dim HouseLocked As Boolean = False

    'Temporary Storage for Away Mode
    Dim currentTempAway As Integer = 23
    Dim roomTempAway As Integer = 17
    Dim kitchenTempAway As Integer = 16
    Dim garageTempAway As Integer = 8
    Dim corridorTempAway As Integer = 18
    Dim bathroomTempAway As Integer = 22
    Dim livingTempAway As Integer = 20

    'Set Initials
    Dim awayModeOn As Boolean = False




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = " My Inteligent House"

        AllHouseLock()


        'Sets starting temperatures
        UpDownCurrentTempSet.Value = currentTemp
        UpDownCurrentTempSet.Maximum = 30
        'currentTempLabel.Text = currentTemp
        TempLabel.Text = currentTemp
        roomTempLabel.Text = roomTemp
        kitchenTempLabel.Text = kitchenTemp
        corridorTempLabel.Text = corridorTemp
        bathroomTempLabel.Text = bathroomTemp
        livingTempLabel.Text = livingTemp
        garageTempLabel.Text = garageTemp
        UpdateMeanTemp()
        updateColorTemp()

        'Sets the Energy Bar by the function
        Energy.Value = EnergyValue
        powerConsume()

        'Sets color Light button
        CorridorON.BackColor = Color.Yellow
        CorridorOFF.BackColor = Color.White
        CorridorOFF.ForeColor = Color.Black
        RoomON.BackColor = Color.Yellow
        RoomOFF.BackColor = Color.White
        RoomOFF.ForeColor = Color.Black
        GarageON.BackColor = Color.White
        GarageOFF.BackColor = Color.Black
        GarageOFF.ForeColor = Color.White
        BathroomON.BackColor = Color.White
        BathroomOFF.BackColor = Color.Black
        BathroomOFF.ForeColor = Color.White
        KitchenON.BackColor = Color.Yellow
        KitchenOFF.BackColor = Color.White
        KitchenOFF.ForeColor = Color.Black
        LivingON.BackColor = Color.Yellow
        LivingOFF.BackColor = Color.White
        LivingOFF.ForeColor = Color.Black

        'Sets Lock House Status
        Warning.Hide()
        Warning2.Hide()
        KeyHouse.Show()

        'Initial Time
        updateTime()

        'Initial Mode
        StatusPic.Image = IntelligentHouse.My.Resources.sun_nano
        Lightsoff()

        KeyHouse.Hide()

    End Sub


    Private Sub updateTime()
        Clock.Text = TimeOfDay
        DateField.Text = DateString
    End Sub

    Private Sub powerConsume()
        'Sets the Energy Bar
        Dim temporaryValue As Integer = 0

        If (LightRoom.BackColor = Color.Yellow) Then
            temporaryValue += 10
        End If

        If (LightCorridor.BackColor = Color.Yellow) Then
            temporaryValue += 10
        End If

        If (LightGarage.BackColor = Color.Yellow) Then
            temporaryValue += 10
        End If

        If (LightBathroom.BackColor = Color.Yellow) Then
            temporaryValue += 10
        End If

        If (LightKitchen.BackColor = Color.Yellow) Then
            temporaryValue += 10
        End If

        If (LightLiving.BackColor = Color.Yellow) Then
            temporaryValue += 10
        End If

        Energy.Value = temporaryValue

    End Sub
    Private Sub updateValLabelTemp()
        'currentTempLabel.Text = currentTemp.ToString
        TempLabel.Text = currentTemp
        roomTempLabel.Text = roomTemp.ToString
        kitchenTempLabel.Text = kitchenTemp
        corridorTempLabel.Text = corridorTemp
        bathroomTempLabel.Text = bathroomTemp
        livingTempLabel.Text = livingTemp
        garageTempLabel.Text = garageTemp

    End Sub
    Private Function checkColor(temp As Integer)
        If (temp < 10) Then
            Return darkColor
        ElseIf (temp < 15) Then
            Return blueColor
        ElseIf (temp < 20) Then
            Return greenColor
        ElseIf (temp < 25) Then
            Return orangeColor
        Else : Return redColor
        End If

    End Function
    Private Sub updateColorTemp()

        kitchenLabel.BackColor = checkColor(kitchenTemp)
        kitchenTempLabel.BackColor = checkColor(kitchenTemp)
        ButtonMinusKitchen.BackColor = checkColor(kitchenTemp)
        ButtonPlusKitchen.BackColor = checkColor(kitchenTemp)

        roomLabel.BackColor = checkColor(roomTemp)
        roomTempLabel.BackColor = checkColor(roomTemp)
        ButtonMinusRoom.BackColor = checkColor(roomTemp)
        ButtonPlusRoom.BackColor = checkColor(roomTemp)

        garageLabel.BackColor = checkColor(garageTemp)
        garageTempLabel.BackColor = checkColor(garageTemp)
        ButtonMinusGarage.BackColor = checkColor(garageTemp)
        ButtonPlusGarage.BackColor = checkColor(garageTemp)

        livingLabel.BackColor = checkColor(livingTemp)
        livingTempLabel.BackColor = checkColor(livingTemp)
        ButtonMinusLiving.BackColor = checkColor(livingTemp)
        ButtonPlusLiving.BackColor = checkColor(livingTemp)

        corridorLabel.BackColor = checkColor(corridorTemp)
        corridorTempLabel.BackColor = checkColor(corridorTemp)
        ButtonMinusCorridor.BackColor = checkColor(corridorTemp)
        ButtonPlusCorridor.BackColor = checkColor(corridorTemp)

        bathroomLabel.BackColor = checkColor(bathroomTemp)
        bathroomTempLabel.BackColor = checkColor(bathroomTemp)
        ButtonMinusBathroom.BackColor = checkColor(bathroomTemp)
        ButtonPlusBathroom.BackColor = checkColor(bathroomTemp)

    End Sub

    Private Function sumTemp(temp As Integer, val As Integer)
        Dim sum As Integer
        sum = temp + val
        If sum < 0 Then
            Return 0
        ElseIf sum > 30 Then
            Return 30
        Else : Return sum
        End If

    End Function

    Private Sub adjustTemp(newTemp As Integer)
        Dim add As Integer

        add = newTemp - currentTemp
        corridorTemp = sumTemp(corridorTemp, add)
        bathroomTemp = sumTemp(bathroomTemp, add)
        livingTemp = sumTemp(livingTemp, add)
        roomTemp = sumTemp(roomTemp, add)
        garageTemp = sumTemp(garageTemp, add)
        kitchenTemp = sumTemp(kitchenTemp, add)

        updateValLabelTemp()
        updateColorTemp()

    End Sub

    Private Sub UpdateMeanTemp()

        currentTemp = (corridorTemp + roomTemp + bathroomTemp + garageTemp + livingTemp + kitchenTemp) / 6
        UpDownCurrentTempSet.Value = currentTemp

    End Sub


    Private Sub UpDownCurrentTempSet_ValueChanged(sender As Object, e As EventArgs) Handles UpDownCurrentTempSet.ValueChanged

        adjustTemp(UpDownCurrentTempSet.Value)
        currentTemp = UpDownCurrentTempSet.Value

    End Sub

    Private Sub ButtonPlusCorridor_Click(sender As Object, e As EventArgs) Handles ButtonPlusCorridor.Click
        If (corridorTemp < 30) Then
            corridorTemp += 1
            corridorTempLabel.Text = corridorTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If

    End Sub

    Private Sub ButtonMinusCorridor_Click(sender As Object, e As EventArgs) Handles ButtonMinusCorridor.Click
        If corridorTemp <> 0 Then
            corridorTemp -= 1
            corridorTempLabel.Text = corridorTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonPlusGarage_Click(sender As Object, e As EventArgs) Handles ButtonPlusGarage.Click
        If (garageTemp < 30) Then
            garageTemp += 1
            garageTempLabel.Text = garageTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonMinusGarage_Click(sender As Object, e As EventArgs) Handles ButtonMinusGarage.Click
        If garageTemp <> 0 Then
            garageTemp -= 1
            garageTempLabel.Text = garageTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonPlusBathroom_Click(sender As Object, e As EventArgs) Handles ButtonPlusBathroom.Click
        If (bathroomTemp < 30) Then
            bathroomTemp += 1
            bathroomTempLabel.Text = bathroomTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonMinusBathroom_Click(sender As Object, e As EventArgs) Handles ButtonMinusBathroom.Click
        If bathroomTemp <> 0 Then
            bathroomTemp -= 1
            bathroomTempLabel.Text = bathroomTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonPlusKitchen_Click(sender As Object, e As EventArgs) Handles ButtonPlusKitchen.Click
        If (kitchenTemp < 30) Then
            kitchenTemp += 1
            kitchenTempLabel.Text = kitchenTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonMinusKitchen_Click(sender As Object, e As EventArgs) Handles ButtonMinusKitchen.Click
        If kitchenTemp <> 0 Then
            kitchenTemp -= 1
            kitchenTempLabel.Text = kitchenTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonPlusLiving_Click(sender As Object, e As EventArgs) Handles ButtonPlusLiving.Click
        If (livingTemp < 30) Then
            livingTemp += 1
            livingTempLabel.Text = livingTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonMinusLiving_Click(sender As Object, e As EventArgs) Handles ButtonMinusLiving.Click
        If livingTemp <> 0 Then
            livingTemp -= 1
            livingTempLabel.Text = livingTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonMinusRoom_Click(sender As Object, e As EventArgs) Handles ButtonMinusRoom.Click
        If roomTemp <> 0 Then
            roomTemp -= 1
            roomTempLabel.Text = roomTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub ButtonPlusRoom_Click(sender As Object, e As EventArgs) Handles ButtonPlusRoom.Click
        If (roomTemp < 30) Then
            roomTemp += 1
            roomTempLabel.Text = roomTemp
            updateColorTemp()
            UpdateMeanTemp()
        End If
    End Sub

    Private Sub RoomON_Click(sender As Object, e As EventArgs) Handles RoomON.Click
        LightRoom.BackColor = Color.Yellow
        LightRoom.ForeColor = Color.Black
        RoomON.BackColor = Color.Yellow
        RoomOFF.BackColor = Color.White
        RoomOFF.ForeColor = Color.Black
        powerConsume()
    End Sub

    Private Sub RoomOFF_Click(sender As Object, e As EventArgs) Handles RoomOFF.Click
        LightRoom.BackColor = Color.Black
        LightRoom.ForeColor = Color.White
        RoomON.BackColor = Color.White
        RoomOFF.BackColor = Color.Black
        RoomOFF.ForeColor = Color.White

        powerConsume()
    End Sub

    Private Sub CorridorON_Click(sender As Object, e As EventArgs) Handles CorridorON.Click
        LightCorridor.BackColor = Color.Yellow
        LightCorridor.ForeColor = Color.Black
        CorridorON.BackColor = Color.Yellow
        CorridorOFF.BackColor = Color.White
        CorridorOFF.ForeColor = Color.Black
        powerConsume()
    End Sub

    Private Sub CorridorOFF_Click(sender As Object, e As EventArgs) Handles CorridorOFF.Click
        LightCorridor.BackColor = Color.Black
        LightCorridor.ForeColor = Color.White
        CorridorON.BackColor = Color.White
        CorridorOFF.BackColor = Color.Black
        CorridorOFF.ForeColor = Color.White
        powerConsume()
    End Sub

    Private Sub GarageON_Click(sender As Object, e As EventArgs) Handles GarageON.Click
        LightGarage.BackColor = Color.Yellow
        LightGarage.ForeColor = Color.Black
        GarageON.BackColor = Color.Yellow
        GarageOFF.BackColor = Color.White
        GarageOFF.ForeColor = Color.Black
        powerConsume()
    End Sub

    Private Sub GarageOFF_Click(sender As Object, e As EventArgs) Handles GarageOFF.Click
        LightGarage.BackColor = Color.Black
        LightGarage.ForeColor = Color.White
        GarageON.BackColor = Color.White
        GarageOFF.BackColor = Color.Black
        GarageOFF.ForeColor = Color.White
        powerConsume()
    End Sub

    Private Sub BathroomON_Click(sender As Object, e As EventArgs) Handles BathroomON.Click
        LightBathroom.BackColor = Color.Yellow
        LightBathroom.ForeColor = Color.Black
        BathroomON.BackColor = Color.Yellow
        BathroomOFF.BackColor = Color.White
        BathroomOFF.ForeColor = Color.Black
        powerConsume()
    End Sub

    Private Sub BathroomOFF_Click(sender As Object, e As EventArgs) Handles BathroomOFF.Click
        LightBathroom.BackColor = Color.Black
        LightBathroom.ForeColor = Color.White
        BathroomON.BackColor = Color.White
        BathroomOFF.BackColor = Color.Black
        BathroomOFF.ForeColor = Color.White
        powerConsume()
    End Sub

    Private Sub KitchenON_Click(sender As Object, e As EventArgs) Handles KitchenON.Click
        LightKitchen.BackColor = Color.Yellow
        LightKitchen.ForeColor = Color.Black
        KitchenON.BackColor = Color.Yellow
        KitchenOFF.BackColor = Color.White
        KitchenOFF.ForeColor = Color.Black
        powerConsume()
    End Sub

    Private Sub KitchenOFF_Click(sender As Object, e As EventArgs) Handles KitchenOFF.Click
        LightKitchen.BackColor = Color.Black
        LightKitchen.ForeColor = Color.White
        KitchenON.BackColor = Color.White
        KitchenOFF.BackColor = Color.Black
        KitchenOFF.ForeColor = Color.White
        powerConsume()
    End Sub

    Private Sub LivingON_Click(sender As Object, e As EventArgs) Handles LivingON.Click
        LightLiving.BackColor = Color.Yellow
        LightLiving.ForeColor = Color.Black
        LivingON.BackColor = Color.Yellow
        LivingOFF.BackColor = Color.White
        LivingOFF.ForeColor = Color.Black
        powerConsume()
    End Sub

    Private Sub LivingOFF_Click(sender As Object, e As EventArgs) Handles LivingOFF.Click
        LightLiving.BackColor = Color.Black
        LightLiving.ForeColor = Color.White
        LivingON.BackColor = Color.White
        LivingOFF.BackColor = Color.Black
        LivingOFF.ForeColor = Color.White
        powerConsume()
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles LabelEnergy.Click

    End Sub

    Private Sub HouseON_Click(sender As Object, e As EventArgs) Handles HouseON.Click
        LightRoom.BackColor = Color.Yellow
        LightRoom.ForeColor = Color.Black
        LightCorridor.BackColor = Color.Yellow
        LightCorridor.ForeColor = Color.Black
        LightGarage.BackColor = Color.Yellow
        LightGarage.ForeColor = Color.Black
        LightBathroom.BackColor = Color.Yellow
        LightBathroom.ForeColor = Color.Black
        LightKitchen.BackColor = Color.Yellow
        LightKitchen.ForeColor = Color.Black
        LightLiving.BackColor = Color.Yellow
        LightLiving.ForeColor = Color.Black

        'Color buttons'
        RoomON.BackColor = Color.Yellow
        RoomOFF.BackColor = Color.White
        RoomOFF.ForeColor = Color.Black

        LivingON.BackColor = Color.Yellow
        LivingOFF.BackColor = Color.White
        LivingOFF.ForeColor = Color.Black

        GarageON.BackColor = Color.Yellow
        GarageOFF.BackColor = Color.White
        GarageOFF.ForeColor = Color.Black

        BathroomON.BackColor = Color.Yellow
        BathroomOFF.BackColor = Color.White
        BathroomOFF.ForeColor = Color.Black

        CorridorON.BackColor = Color.Yellow
        CorridorOFF.BackColor = Color.White
        CorridorOFF.ForeColor = Color.Black

        KitchenON.BackColor = Color.Yellow
        KitchenOFF.BackColor = Color.White
        KitchenOFF.ForeColor = Color.Black

        powerConsume()
    End Sub

    Private Sub HouseOFF_Click(sender As Object, e As EventArgs) Handles HouseOFF.Click
        LightRoom.BackColor = Color.Black
        LightRoom.ForeColor = Color.White
        LightCorridor.BackColor = Color.Black
        LightCorridor.ForeColor = Color.White
        LightGarage.BackColor = Color.Black
        LightGarage.ForeColor = Color.White
        LightBathroom.BackColor = Color.Black
        LightBathroom.ForeColor = Color.White
        LightKitchen.BackColor = Color.Black
        LightKitchen.ForeColor = Color.White
        LightLiving.BackColor = Color.Black
        LightLiving.ForeColor = Color.White

        'Color Buttons'
        RoomON.BackColor = Color.White
        RoomOFF.BackColor = Color.Black
        RoomOFF.ForeColor = Color.White

        BathroomON.BackColor = Color.White
        BathroomOFF.BackColor = Color.Black
        BathroomOFF.ForeColor = Color.White

        KitchenON.BackColor = Color.White
        KitchenOFF.BackColor = Color.Black
        KitchenOFF.ForeColor = Color.White

        LivingON.BackColor = Color.White
        LivingOFF.BackColor = Color.Black
        LivingOFF.ForeColor = Color.White

        GarageON.BackColor = Color.White
        GarageOFF.BackColor = Color.Black
        GarageOFF.ForeColor = Color.White

        CorridorON.BackColor = Color.White
        CorridorOFF.BackColor = Color.Black
        CorridorOFF.ForeColor = Color.White

        powerConsume()
    End Sub

    Private Sub Lightsoff()
        LightRoom.BackColor = Color.Black
        LightRoom.ForeColor = Color.White
        LightCorridor.BackColor = Color.Black
        LightCorridor.ForeColor = Color.White
        LightGarage.BackColor = Color.Black
        LightGarage.ForeColor = Color.White
        LightBathroom.BackColor = Color.Black
        LightBathroom.ForeColor = Color.White
        LightKitchen.BackColor = Color.Black
        LightKitchen.ForeColor = Color.White
        LightLiving.BackColor = Color.Black
        LightLiving.ForeColor = Color.White

        'Color Buttons'
        RoomON.BackColor = Color.White
        RoomOFF.BackColor = Color.Black
        RoomOFF.ForeColor = Color.White

        BathroomON.BackColor = Color.White
        BathroomOFF.BackColor = Color.Black
        BathroomOFF.ForeColor = Color.White

        KitchenON.BackColor = Color.White
        KitchenOFF.BackColor = Color.Black
        KitchenOFF.ForeColor = Color.White

        LivingON.BackColor = Color.White
        LivingOFF.BackColor = Color.Black
        LivingOFF.ForeColor = Color.White

        GarageON.BackColor = Color.White
        GarageOFF.BackColor = Color.Black
        GarageOFF.ForeColor = Color.White

        CorridorON.BackColor = Color.White
        CorridorOFF.BackColor = Color.Black
        CorridorOFF.ForeColor = Color.White

        powerConsume()
    End Sub





    Private Sub ButtonPlusTemp_Click(sender As Object, e As EventArgs) Handles ButtonPlusTemp.Click
        If (currentTemp < 30) Then
            adjustTemp(TempLabel.Text + 1)
            currentTemp += 1
            TempLabel.Text = currentTemp
        End If
    End Sub

    Private Sub ButtonMinusTemp_Click(sender As Object, e As EventArgs) Handles ButtonMinusTemp.Click
        If (currentTemp > 0) Then
            adjustTemp(TempLabel.Text - 1)
            currentTemp -= 1
            TempLabel.Text = currentTemp

        End If
    End Sub

    Private Sub TempEachRoomButton_Click(sender As Object, e As EventArgs) Handles TempEachRoomButton.Click
        TempEachRoom()
    End Sub

    Private Sub TempEachRoom()
        roomTemp = currentTemp
        livingTemp = currentTemp
        corridorTemp = currentTemp
        garageTemp = currentTemp
        bathroomTemp = currentTemp
        kitchenTemp = currentTemp
        updateValLabelTemp()
        updateColorTemp()
        UpdateMeanTemp()
    End Sub
    Private Sub AllHouseOpen()
        RoomLOCK.BackColor = Color.Red
        BathroomLOCK.BackColor = Color.Red
        GarageLOCK.BackColor = Color.Red
        KitchenLOCK.BackColor = Color.Red
        CorridorLOCK.BackColor = Color.Red
        LivingLOCK.BackColor = Color.Red
        'KeyRoom.Visible = False

        LabelRoom.Text = "OPEN"
        LabelRoom.ForeColor = Color.Red
        LabelBathroom.Text = "OPEN"
        LabelBathroom.ForeColor = Color.Red
        LabelGarage.Text = "OPEN"
        LabelGarage.ForeColor = Color.Red
        LabelKitchen.Text = "OPEN"
        LabelKitchen.ForeColor = Color.Red
        LabelCorridor.Text = "OPEN"
        LabelCorridor.ForeColor = Color.Red
        LabelLiving.Text = "OPEN"
        LabelLiving.ForeColor = Color.Red

        Timer1.Enabled = True
        HousePanel.ForeColor = Color.Red
        HousePanel.Text = "OPEN"
        KeyHouse.Hide()
        Warning.Show()
        Warning2.Hide()
        changelockstatus("Open")
    End Sub



    Private Sub HouseOPEN_Click(sender As Object, e As EventArgs) Handles HouseOPEN.Click
        AllHouseOpen()

    End Sub

    Private Sub AllHouseLock()
        RoomLOCK.BackColor = Color.SteelBlue
        BathroomLOCK.BackColor = Color.SteelBlue
        GarageLOCK.BackColor = Color.SteelBlue
        KitchenLOCK.BackColor = Color.SteelBlue
        CorridorLOCK.BackColor = Color.SteelBlue
        LivingLOCK.BackColor = Color.SteelBlue
        'KeyRoom.Visible = True

        LabelRoom.Text = "LOCKED"
        LabelRoom.ForeColor = Color.SteelBlue
        LabelBathroom.Text = "LOCKED"
        LabelBathroom.ForeColor = Color.SteelBlue
        LabelGarage.Text = "LOCKED"
        LabelGarage.ForeColor = Color.SteelBlue
        LabelKitchen.Text = "LOCKED"
        LabelKitchen.ForeColor = Color.SteelBlue
        LabelCorridor.Text = "LOCKED"
        LabelCorridor.ForeColor = Color.SteelBlue
        LabelLiving.Text = "LOCKED"
        LabelLiving.ForeColor = Color.SteelBlue

        Timer1.Enabled = False
        HousePanel.ForeColor = Color.Green
        HousePanel.Text = "LOCKED"
        Warning.Hide()
        Warning2.Hide()
        KeyHouse.Show()
        changelockstatus("Locked")
    End Sub

    Private Sub changelockstatus(status As String)
        Label13.Text = status

    End Sub

    Private Sub HouseLOCK_Click(sender As Object, e As EventArgs) Handles HouseLOCK.Click
        AllHouseLock()


    End Sub


    Private Sub OpenerBathroom_Click(sender As Object, e As EventArgs) Handles OpenerBathroom.Click
        BathroomLOCK.BackColor = Color.Red
        LabelBathroom.Text = "OPEN"
        LabelBathroom.ForeColor = Color.Red
        CheckHouseStatus()
    End Sub


    Private Sub LockerBathroom_Click(sender As Object, e As EventArgs) Handles LockerBathroom.Click
        BathroomLOCK.BackColor = Color.SteelBlue
        LabelBathroom.Text = "LOCKED"
        LabelBathroom.ForeColor = Color.SteelBlue
        CheckHouseStatus()
    End Sub


    Private Sub LockerGarage_Click(sender As Object, e As EventArgs) Handles LockerGarage.Click
        GarageLOCK.BackColor = Color.SteelBlue
        LabelGarage.Text = "LOCKED"
        LabelGarage.ForeColor = Color.SteelBlue
        CheckHouseStatus()
    End Sub

    Private Sub OpenerGarage_Click(sender As Object, e As EventArgs) Handles OpenerGarage.Click
        GarageLOCK.BackColor = Color.Red
        LabelGarage.Text = "OPEN"
        LabelGarage.ForeColor = Color.Red
        CheckHouseStatus()
    End Sub

    Private Sub LockerCorridor_Click(sender As Object, e As EventArgs) Handles LockerCorridor.Click
        CorridorLOCK.BackColor = Color.SteelBlue
        LabelCorridor.Text = "LOCKED"
        LabelCorridor.ForeColor = Color.SteelBlue
        CheckHouseStatus()
    End Sub

    Private Sub OpenerCorridor_Click(sender As Object, e As EventArgs) Handles OpenerCorridor.Click
        CorridorLOCK.BackColor = Color.Red
        LabelCorridor.Text = "OPEN"
        LabelCorridor.ForeColor = Color.Red
        CheckHouseStatus()
    End Sub

    Private Sub LockerRoom_Click(sender As Object, e As EventArgs) Handles LockerRoom.Click
        RoomLOCK.BackColor = Color.SteelBlue
        LabelRoom.Text = "LOCKED"
        LabelRoom.ForeColor = Color.SteelBlue
        CheckHouseStatus()
    End Sub

    Private Sub OpenerRoom_Click(sender As Object, e As EventArgs) Handles OpenerRoom.Click
        RoomLOCK.BackColor = Color.Red
        LabelRoom.Text = "OPEN"
        LabelRoom.ForeColor = Color.Red
        CheckHouseStatus()
    End Sub

    Private Sub LockerLiving_Click(sender As Object, e As EventArgs) Handles LockerLiving.Click
        LivingLOCK.BackColor = Color.SteelBlue
        LabelLiving.Text = "LOCKED"
        LabelLiving.ForeColor = Color.SteelBlue
        CheckHouseStatus()
    End Sub

    Private Sub OpenerLiving_Click(sender As Object, e As EventArgs) Handles OpenerLiving.Click
        LivingLOCK.BackColor = Color.Red
        LabelLiving.Text = "OPEN"
        LabelLiving.ForeColor = Color.Red
        CheckHouseStatus()
    End Sub

    Private Sub LockerKitchen_Click(sender As Object, e As EventArgs) Handles LockerKitchen.Click
        KitchenLOCK.BackColor = Color.SteelBlue
        LabelKitchen.Text = "LOCKED"
        LabelKitchen.ForeColor = Color.SteelBlue
        CheckHouseStatus()
    End Sub

    Private Sub OpenerKitchen_Click(sender As Object, e As EventArgs) Handles OpenerKitchen.Click
        KitchenLOCK.BackColor = Color.Red
        LabelKitchen.Text = "OPEN"
        LabelKitchen.ForeColor = Color.Red
        CheckHouseStatus()
    End Sub

    Private Sub CheckHouseStatus()
        If (KitchenLOCK.BackColor = Color.SteelBlue And LivingLOCK.BackColor = Color.SteelBlue And RoomLOCK.BackColor = Color.SteelBlue And CorridorLOCK.BackColor = Color.SteelBlue And GarageLOCK.BackColor = Color.SteelBlue And BathroomLOCK.BackColor = Color.SteelBlue) Then
            Timer1.Enabled = False
            HousePanel.ForeColor = Color.Green
            HousePanel.Text = "LOCKED"
            Warning.Hide()
            Warning2.Hide()
            KeyHouse.Show()
            changelockstatus("Locked")
        Else
            HousePanel.ForeColor = Color.Red
            HousePanel.Text = "OPEN"
            Timer1.Enabled = True
            KeyHouse.Hide()
            Warning.Show()
            Warning2.Show()
            changelockstatus("Open")
        End If

    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Static time As Integer
        time = time + 1
        If time = 1 Then
            HousePanel.ForeColor = Color.Red
            Warning.Show()
            Warning2.Show()
        ElseIf time = 2 Then
            HousePanel.ForeColor = Color.Orange
            Warning.Hide()
            Warning2.Hide()
        ElseIf time = 3 Then
            HousePanel.ForeColor = Color.Yellow
            Warning.Show()
            Warning2.Show()
        Else : time = 4
            Warning.Hide()
            Warning2.Hide()
            time = 0
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        updateTime()
        CurrTemp.Text = currentTemp
        Me.SetDesktopLocation(10, 10)

    End Sub


    Private Sub SummerButton_Click(sender As Object, e As EventArgs) Handles SummerButton.Click
        If awayModeOn Then
            AwayTimer.Enabled = False
            currentTemp = currentTempAway
            roomTemp = roomTempAway
            kitchenTemp = kitchenTempAway
            garageTemp = garageTempAway
            corridorTemp = corridorTempAway
            bathroomTemp = bathroomTempAway
            livingTemp = livingTempAway
            awayModeOn = False

            updateValLabelTemp()
            updateColorTemp()
            UpdateMeanTemp()
            AllHouseOpen()
        End If

        StatusPic.Image = IntelligentHouse.My.Resources.sun_nano
    End Sub

    Private Sub WinterButton_Click(sender As Object, e As EventArgs) Handles WinterButton.Click
        If awayModeOn Then
            AwayTimer.Enabled = False
            currentTemp = currentTempAway
            roomTemp = roomTempAway
            kitchenTemp = kitchenTempAway
            garageTemp = garageTempAway
            corridorTemp = corridorTempAway
            bathroomTemp = bathroomTempAway
            livingTemp = livingTempAway
            awayModeOn = False
            AllHouseOpen()

            updateValLabelTemp()
            updateColorTemp()
            UpdateMeanTemp()
        End If
        StatusPic.Image = IntelligentHouse.My.Resources.ice_nano

    End Sub

    Private Sub AwayButton_Click(sender As Object, e As EventArgs) Handles AwayButton.Click
        Warning2.Hide()
        StatusPic.Image = IntelligentHouse.My.Resources.away_nano
        currentTempAway = currentTemp
        roomTempAway = roomTemp
        kitchenTempAway = kitchenTemp
        garageTempAway = garageTemp
        corridorTempAway = corridorTemp
        bathroomTempAway = bathroomTemp
        livingTempAway = livingTemp

        currentTemp = AwayTemp.Value
        TempEachRoom()
        AwayTimer.Enabled = True
        awayModeOn = True

        AllHouseLock()
        Lightsoff()



    End Sub

 
    Private Sub AwayTimer_Tick(sender As Object, e As EventArgs) Handles AwayTimer.Tick
        currentTemp = AwayTemp.Value
        TempEachRoom()
    End Sub

End Class
