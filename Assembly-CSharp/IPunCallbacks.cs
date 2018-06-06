﻿// Decompiled with JetBrains decompiler
// Type: IPunCallbacks
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

public interface IPunCallbacks
{
  void OnConnectedToPhoton();

  void OnLeftRoom();

  void OnMasterClientSwitched(PhotonPlayer newMasterClient);

  void OnPhotonCreateRoomFailed(object[] codeAndMsg);

  void OnPhotonJoinRoomFailed(object[] codeAndMsg);

  void OnCreatedRoom();

  void OnJoinedLobby();

  void OnLeftLobby();

  void OnFailedToConnectToPhoton(DisconnectCause cause);

  void OnConnectionFail(DisconnectCause cause);

  void OnDisconnectedFromPhoton();

  void OnPhotonInstantiate(PhotonMessageInfo info);

  void OnReceivedRoomListUpdate();

  void OnJoinedRoom();

  void OnPhotonPlayerConnected(PhotonPlayer newPlayer);

  void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer);

  void OnPhotonRandomJoinFailed(object[] codeAndMsg);

  void OnConnectedToMaster();

  void OnPhotonMaxCccuReached();

  void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged);

  void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps);

  void OnUpdatedFriendList();

  void OnCustomAuthenticationFailed(string debugMessage);

  void OnCustomAuthenticationResponse(Dictionary<string, object> data);

  void OnWebRpcResponse(OperationResponse response);

  void OnOwnershipRequest(object[] viewAndPlayer);

  void OnLobbyStatisticsUpdate();
}
