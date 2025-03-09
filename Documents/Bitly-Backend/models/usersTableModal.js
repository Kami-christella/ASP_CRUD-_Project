import mongoose from "mongoose";
import Counter from "./counter.js"; // Correct import for the Counter model

const { model, Schema } = mongoose;

const userSchema = new Schema(
  {
    userId: { type: Number, unique: true }, // Auto-incremented ID
    userName: {
      type: String,
      required: true
    },
    userEmail: {
      type: String,
      required: true
    },
    userPassword: {
      type: String,
      required: true
    },
    userRole: {
      type: String,
      required: false,
      default: "user",
      enum: ["user", "admin"]
    },
    tokens: {
      accessToken: {
        type: String,
        default: ""
      }
    }
  },
  {
    timestamps: true
  }
);

// Pre-save hook to auto-increment userId
userSchema.pre("save", async function (next) {
  if (!this.userId) { // Only set userId if it is not already set
    const counter = await Counter.findByIdAndUpdate(
      { _id: "userId" },
      { $inc: { seq: 1 } }, // Increment the sequence number
      { new: true, upsert: true }
    );
    this.userId = counter.seq; // Assign the incremented ID to userId
  }
  next();
});

// Export the model
const User = model("Bitlyuser", userSchema);
export default User;
