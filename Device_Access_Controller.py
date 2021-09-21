#Global lists
login_list=[]
block_list=[]
def login(devices):
    """
    This function takes the device name as argument and
    checks the current status of device before logging it in.
    """
    #Use of global keyword to modify a global variable
    global login_list
    global block_list
    if devices in block_list:
        #If the device is blocked, restrict the login
        print(device," is blocked, cannot login")
    elif devices in login_list:
        #If the device is already logged in
        print(devices," already logged in")
    else:
        #If the device is clear to get logged in
        login_list.append(device)
        #Prompt the user that login is successfull
        print(devices,"logged in")
def logout(devices):
    """
    This function takes the device name as argument and checks
    the device's current status and logs out the device.
    """
    global login_list
    global block_list
    if devices in block_list:
        #If the device is blocked, prompt the user that
        #device is blocked
        print(devices,"is blocked")
    elif devices not in login_list:
        #If the device is not logged in, it cannot be logged out
        #This condition is checked here
        print(devices," not logged in")
    else:
        #Device is ready to log out
        login_list.remove(device)
        #Prompt the user that device is logged out successfully
        print(devices,"logged out ")
def block(devices):
    """
    This function takes the device name as argument
    and blocks the device such that further logins from
    that device are restricted.
    """
    global login_list
    global block_list
    if devices in login_list:
        #If device is logged in and need to block it
        login_list.remove(device)
    #If device is not logged in and need to block it
    #Simply block it
    block_list.append(device)
    print(devices,"is blocked successfully!")
if __name__=="__main__":
    #Driver code to implement the above functionalities
    #Specification of input format
    print("Enter device:action to be performed")
    print("Type exit when you are done")
    #logins list to store the device names which are currently logged in
    logins=[]
    logouts=[]
    blocks=[]
    #Iterating infinitely
    while(True):
        device_action=input().split(":")
        #Invalid input format causes loop to terminate
        if(len(device_action)==1):
            break
        #Removing extra spaces
        device=device_action[0].strip()
        action=device_action[1].strip().lower()
        #Set of conditions to trigger appropriate function call
        if action=="login":
            login(device)
        elif action=="logout":
            logout(device)
        elif action=="block":
            block(device)
        else:
            print("Check your input!")
    #Output formatting
    if not login_list:
        print("No Active Devices")
    else:
        print("Active Devices: ",*login_list)
    if not block_list:
        print("No Blocked devices")
    else:
        print("Blocked Devices: ",*block_list)