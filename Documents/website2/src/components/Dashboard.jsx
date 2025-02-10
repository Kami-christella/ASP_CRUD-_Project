import { IoPersonCircleSharp } from "react-icons/io5";
import { RxDragHandleDots2 } from "react-icons/rx";
import { IoIosAdd } from "react-icons/io";
import { CiMenuKebab } from "react-icons/ci";
import '../styles/Dashboard.css'


function Dashboard(){
    return(
        <section className="allClasses">
        <div className="leftClass">
          <div><IoPersonCircleSharp /> Login</div>
          <hr></hr>
          <div> All Boards(4)</div>
          <div><RxDragHandleDots2/>Platform Launch</div>
          <div><RxDragHandleDots2/>Marketing Plan</div>
          <div><RxDragHandleDots2/>Roadmap</div>
          <div> <IoIosAdd /> Create New Board</div>
         
        </div>
        <hr></hr>
        <div className="rightClass">
          <div>
             <span>Platform Launch</span>
             <button> <IoIosAdd />Add New Task </button>
             <CiMenuKebab />
             <hr></hr>
          </div>
        </div>
        </section>
    )
}

export default Dashboard