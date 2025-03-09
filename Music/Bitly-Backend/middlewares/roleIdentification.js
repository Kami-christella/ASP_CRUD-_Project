export const Admin = (req, res, next)=>{
    if(req.User.role  !== 'admin'){
        return res.status(403).json({message:"Access Denied contact Admin Please"});
    }
    next();
}
