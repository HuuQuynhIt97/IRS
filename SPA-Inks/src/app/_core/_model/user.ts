export interface User {
  ID: number;
  Username: string;
  Alias: string;
  image: string;
  IsLeader: boolean;
  OcLevel: number;
  Role: number;
  ListOcs: [];
}
export interface UserGetAll {
   ID: number;
   Username: string;
   OCID: number ;
   LevelOC: number ;
   Email: string ;
   RoleID: number ;
   ImageURL: string ;
  ImageBase64: string   ;
  isLeader: boolean ;
   Role: any ;
}

export interface UserForLogin {
  username: string;
  password: string;
  systemCode: number;

}
export interface SignInHistory {
  id: number;
  username: string;
  userID: number;
  token: string;
  systemCode: number;
  loginTime: Date;
  lastRequestEndTime: Date;
  createdDate: Date;
}
