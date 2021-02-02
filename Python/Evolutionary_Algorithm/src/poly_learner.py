import random
import numpy as np
import sys
from matplotlib import pyplot

class Node:
    def __init__(self, coeffs):
        self.coeffs = coeffs

    def get_fitness(self, real_coeffs, x):
        real_y = run_function(x,real_coeffs)
        our_y = run_function(x,self.coeffs)

        fitness = 0;
        for i in range(len(real_y)):
            fitness += abs(our_y[i]-real_y[i])
        fitness = fitness / len(our_y)

        return fitness

def generate_function(l):
    coeffs = []
    for i in range(l):
        coeffs.append(random.randint(-20,20))
    return coeffs

def run_function(x, coeffs):
    y = 0
    for i in range(len(coeffs)):
        y += coeffs[i] * x ** i
    return y

def generate_nodes(population_size, l):
    ns = []
    for i in range(population_size):
        ns.append(Node(generate_function(l)))
    return ns

def draw_plot(nodes, real_coeffs, x, ax, fig, generation):
    ax.clear()
    text = f"Generation: {generation}\nBest: {nodes[0].get_fitness(real_coeffs, x)}"
    ax.plot(x, run_function(x, real_coeffs), '-b', label="Real")
    ax.plot(x, run_function(x, nodes[0].coeffs), '-r', label="Best")
    ax.text(0.02, 0.98, text, transform=ax.transAxes, verticalalignment='top')
    ax.legend(loc="upper right")
    fig.canvas.draw()
    pyplot.pause(0.5)

def make_children(parents):
    children = []
    i = 0
    while i < len(parents):
        child = get_child(parents[i], parents[i+1])
        children.append(child)
        i += 2
    return children

def get_child(p1, p2):
    coeffs = []

    for i in range(len(p1.coeffs)):
        chance = random.randint(0,1)
        if chance == 1:
            coeffs.append(p1.coeffs[i])
        else:
            coeffs.append(p2.coeffs[i])

    for i in range(len(coeffs)):
        chance = random.randint(0,10)
        if chance == 0:
            coeffs[i] = random.randint(-20, 20)

    return Node(coeffs)

def get_survivors(nodes, pop_size):
    definite = int(pop_size/4)
    survivors = nodes[:definite]
    del nodes[-definite:]
    rest = nodes[definite:]
    i = 0
    while len(survivors) + len(rest) > pop_size and i < len(rest):
        if random.random() > i/len(nodes):
            survivors.append(rest[i])
        del rest[i]
        i += 1
    survivors += rest
    return survivors[:pop_size]

def run_generation(nodes, real_coeffs, x, ax, fig, generation):    
    draw_plot(nodes, real_coeffs, x, ax, fig, generation)
    nodes = sorted(nodes, key=lambda n: n.get_fitness(real_coeffs, x))
    pop_size = len(nodes)
    new_nodes = make_children(nodes[:int(pop_size/2)])
    nodes = nodes + new_nodes
    nodes = sorted(nodes, key=lambda n: n.get_fitness(real_coeffs, x))
    return get_survivors(nodes, pop_size), generation

def pretty_print(node):
    func = ""
    for i in range(len(node.coeffs)):
        if i == 0:
            func = f" + {node.coeffs[i]}" + func
        elif i == 1:
            func = f" + {node.coeffs[i]}x" + func
        else:
            func = f" + {node.coeffs[i]}x^{i}" + func
    func = "y = " + func[3:]
    return func

if __name__ == "__main__":
    order = int(sys.argv[1]) + 1
    gen_size = int(sys.argv[2])
    
    pyplot.ion()
    real_coeffs = generate_function(order)
    nodes = generate_nodes(gen_size, order)
    x = np.linspace(-20,20,40)
    fig = pyplot.figure()
    ax = fig.add_subplot(111)
    test_value = 0
    generation = 0

    while nodes[0].get_fitness(real_coeffs, x) > test_value:
        generation += 1
        nodes, generation = run_generation(nodes, real_coeffs, x, ax, fig, generation)

    print(f"Finished in {generation} generations!\nThe learned formula was {pretty_print(nodes[0])}")
