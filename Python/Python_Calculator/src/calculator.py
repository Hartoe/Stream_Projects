import tkinter as tk
from tkinter import font
import math

DIGITS = '0123456789.'
pi = math.pi
e = math.e

def init():
    #Initialize window
    root = tk.Tk()
    root.title("Calculator")
    root.resizable(False, False)
    root.iconbitmap('../images/icon.ico')

    #Make variables
    output_font = font.Font(family="Fixedsys", size=18)
    button_font = font.Font(family="Fixedsys", size=16)
    small_button_font = font.Font(family="Fixedsys", size=10)

    #Make output frame
    output_frame = tk.Frame(root)
    output_frame.grid(column=0,row=0,columnspan=3)

    output = tk.StringVar()
    output_entry = tk.Entry(output_frame, state='readonly', width=20, font=output_font, justify='right',
                            bd=5, bg='pale turquoise', textvariable=output)
    output_entry.grid(column=0,row=0,padx=10,pady=10,ipady=5)

    #Make power frame
    power_frame = tk.Frame(root)
    power_frame.grid(column=0,row=1,sticky='e')

    sqrt_button = tk.Button(power_frame, text="root", font=small_button_font, command= lambda: add_to_expression(output, "root("))
    sqrt_button.grid(column=0,row=0)
    square_button = tk.Button(power_frame, text="n^2", font=small_button_font, command= lambda: add_to_expression(output, "square("))
    square_button.grid(column=1,row=0)
    power_button = tk.Button(power_frame, text="n^x", font=small_button_font, command= lambda: add_to_expression(output, "power("))
    power_button.grid(column=2, row=0)

    #Make logarithmic frame
    log_frame = tk.Frame(root)
    log_frame.grid(column=0,row=2,sticky='e')

    ln_button = tk.Button(log_frame, text="ln", font=small_button_font, command= lambda: add_to_expression(output, "ln("))
    ln_button.grid(column=0,row=0)
    log10_button = tk.Button(log_frame, text="log", font=small_button_font, command= lambda: add_to_expression(output, "log("))
    log10_button.grid(column=1,row=0)
    log_button = tk.Button(log_frame, text="log_b", font=small_button_font, command= lambda: add_to_expression(output, "log_b("))
    log_button.grid(column=2,row=0)

    #Make trigonometry frame
    trig_frame = tk.Frame(root)
    trig_frame.grid(column=2,row=1,rowspan=2,pady=5)

    sin_button = tk.Button(trig_frame, text="sin", font=small_button_font, command= lambda: add_to_expression(output, "sin("))
    cos_button = tk.Button(trig_frame, text="cos", font=small_button_font, command= lambda: add_to_expression(output, "cos("))
    tan_button = tk.Button(trig_frame, text="tan", font=small_button_font, command= lambda: add_to_expression(output, "tan("))
    asin_button = tk.Button(trig_frame, text="asin", font=small_button_font, command= lambda: add_to_expression(output, "asin("))
    acos_button = tk.Button(trig_frame, text="acos", font=small_button_font, command= lambda: add_to_expression(output, "acos("))
    atan_button = tk.Button(trig_frame, text="atan", font=small_button_font, command= lambda: add_to_expression(output, "atan("))
    sin_button.grid(column=0,row=0)
    cos_button.grid(column=1,row=0)
    tan_button.grid(column=2,row=0)
    asin_button.grid(column=0,row=1)
    acos_button.grid(column=1,row=1)
    atan_button.grid(column=2,row=1)

    #Make constants frame
    constants_frame = tk.Frame(root)
    constants_frame.grid(column=1,row=2,pady=5)

    pi_button = tk.Button(constants_frame, text="pi", font=small_button_font, command= lambda: add_to_expression(output, "pi"))
    pi_button.grid(column=0,row=0)
    e_button = tk.Button(constants_frame, text="e", font=small_button_font, command= lambda: add_to_expression(output, "e"))
    e_button.grid(column=1,row=0)

    #Make numeric frame
    number_frame = tk.Frame(root)
    number_frame.grid(column=0,columnspan=2,row=3)

    seven_button = tk.Button(number_frame, text="7", font=button_font, command= lambda: add_to_expression(output, "7"))
    eight_button = tk.Button(number_frame, text="8", font=button_font, command= lambda: add_to_expression(output, "8"))
    nine_button = tk.Button(number_frame, text="9", font=button_font, command= lambda: add_to_expression(output, "9"))
    seven_button.grid(column=0,row=0,ipadx=10,ipady=10)
    eight_button.grid(column=1,row=0,ipadx=10,ipady=10)
    nine_button.grid(column=2,row=0,ipadx=10,ipady=10)

    six_button = tk.Button(number_frame, text="6", font=button_font, command= lambda: add_to_expression(output, "6"))
    five_button = tk.Button(number_frame, text="5", font=button_font, command= lambda: add_to_expression(output, "5"))
    four_button = tk.Button(number_frame, text="4", font=button_font, command= lambda: add_to_expression(output, "4"))
    six_button.grid(column=0,row=1,ipadx=10,ipady=10)
    five_button.grid(column=1,row=1,ipadx=10,ipady=10)
    four_button.grid(column=2,row=1,ipadx=10,ipady=10)

    three_button = tk.Button(number_frame, text="3", font=button_font, command= lambda: add_to_expression(output, "3"))
    two_button = tk.Button(number_frame, text="2", font=button_font, command= lambda: add_to_expression(output, "2"))
    one_button = tk.Button(number_frame, text="1", font=button_font, command= lambda: add_to_expression(output, "1"))
    three_button.grid(column=0,row=2,ipadx=10,ipady=10)
    two_button.grid(column=1,row=2,ipadx=10,ipady=10)
    one_button.grid(column=2,row=2,ipadx=10,ipady=10)

    clear_button = tk.Button(number_frame, text="C", font=button_font, command= lambda: clear_expression(output))
    zero_button = tk.Button(number_frame, text="0", font=button_font, command= lambda: add_to_expression(output, "0"))
    equal_button = tk.Button(number_frame, text="=", font=button_font, command= lambda: evaluate_expression(output))
    clear_button.grid(column=0,row=3,ipadx=10,ipady=10)
    zero_button.grid(column=1,row=3,ipadx=10,ipady=10)
    equal_button.grid(column=2,row=3,ipadx=10,ipady=10)

    #Make basic operator frame
    operator_frame = tk.Frame(root)
    operator_frame.grid(column=2,row=3)

    add_button = tk.Button(operator_frame, text="+", font=button_font, command= lambda: add_to_expression(output, "+"))
    sub_button = tk.Button(operator_frame, text="-", font=button_font, command= lambda: add_to_expression(output, "-"))
    mult_button = tk.Button(operator_frame, text="*", font=button_font, command= lambda: add_to_expression(output, "*"))
    div_button = tk.Button(operator_frame, text="/", font=button_font, command= lambda: add_to_expression(output, "/"))
    add_button.grid(column=0,row=0,ipadx=10,ipady=10)
    sub_button.grid(column=0,row=1,ipadx=10,ipady=10)
    mult_button.grid(column=0,row=2,ipadx=10,ipady=10)
    div_button.grid(column=0,row=3,ipadx=10,ipady=10)

    #Make logistic frame
    logistic_frame = tk.Frame(root)
    logistic_frame.grid(column=0,columnspan=2,row=4,pady=5)

    left_paren_button = tk.Button(logistic_frame, text="(", font=button_font, command= lambda: add_to_expression(output, "("))
    right_paren_button = tk.Button(logistic_frame, text=")", font=button_font, command= lambda: add_to_expression(output, ")"))
    comma_button = tk.Button(logistic_frame, text=",", font=button_font, command= lambda: add_to_expression(output, ","))
    left_paren_button.grid(column=0,row=0)
    right_paren_button.grid(column=1,row=0)
    comma_button.grid(column=2,row=0)

    return root

#Calculator Methods
def add_to_expression(output, next_op):
    if next_op in ["power(", "square(", "root("]:
        index = len(output.get()) - 1
 
        if index < 0:
            output.set(next_op)
            return

        while output.get()[index] in DIGITS and index >= 0:
            index -= 1

        if index < 0:
            output.set(next_op + output.get())
        else:
            output.set(output.get()[:index+1] + next_op + output.get()[index+1:])
    else:
        if len(output.get()) > 0:
            index = len(output.get()) - 1
            if output.get()[index] in 'ie':
                output.set(output.get() + "*" + next_op)
            elif next_op in ["pi", "e"]:
                if output.get()[index] not in '+-/*,':
                    output.set(output.get() + "*" + next_op)
                else:
                    output.set(output.get() + next_op)
            else:
                output.set(output.get() + next_op)
        else:
            output.set(next_op)

def clear_expression(output):
    output.set("")

def evaluate_expression(output):
    output.set(eval(output.get()))

#Operator Methods
def root(x):
    return math.sqrt(x)

def square(x):
    return power(2,x)

def power(p, b):
    return b ** p

def ln(x):
    return log_b(e(),x)

def log(x):
    return log_b(10, x)

def log_b(b, x):
    return math.log(x, b)

def sin(x):
    return math.sin(x)

def cos(x):
    return math.cos(x)

def tan(x):
    return math.tan(x)

def asin(x):
    return math.asin(x)

def acos(x):
    return math.acos(x)

def atan(x):
    return math.atan(x)

if __name__ == "__main__":
    window = init()
    window.mainloop()
