import authApi from "./authApi"

interface FullUser{
    id:string,
    email:string,
    role: string
}

export const AuthServer = {
    async getToken(){
        const response = await authApi.post(`/connect/token`)
        return response;
    },
    async getUserInfo(){
        try{
            const response = await authApi.get<FullUser>(`/connect/userinfo`)
            return response.data;
        }catch(error){
            console.error("AuthServer.getUserById error:", error);
            throw error; 
        }
    }
}