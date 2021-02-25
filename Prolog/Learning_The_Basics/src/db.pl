% Natural numbers.
nat(0). % Zero is a natural number.
nat(succ(X)) :- nat(X). % Any number is natural if it is a succession of a natural number.

gt(succ(_), 0). % Any succession is greater than 0.
gt(succ(X), succ(Y)) :- gt(X, Y). % X is greater than Y if their predecessors are.
eq(0, 0). % Zero is equal to Zero.
eq(succ(X), succ(Y)) :- eq(X, Y). % X is equal to Y if their predecessors are.
lt(X, Y) :- not(gt(X,Y)), not(eq(X,Y)). % X is less than Y if X is not greater than or equal to Y.

add(0,N,N). % The sum of Zero and X is X.
add(succ(X), Y, succ(Z)) :- add(X, Y, Z). % The sum of X and Y is Z if the sum of the predecessor of X and Y is the predecessor of Z.
sub(N,0,N). % X minus 0 is X.
sub(X, succ(Y), Z) :- sub(X, Y, succ(Z)). % X minus Y is Z if X minus the predecessor of Y is the successor of Z.

even(0). % Zero is even.
even(succ(succ(X))) :- even(X). % X is even if the double predecessor of X is even.
odd(X) :- not(even(X)). % X is odd if it is not even.

mult(_,0,0). % Anything multiplied by Zero is Zero.
mult(N,succ(0),N). % X times 1 is X.
mult(X, succ(Y), Z) :- mult(X, Y, D), add(D, X, Z). % X times Y is Z if X times pre Y is D and D plus X is Z.
square(X, Y) :- mult(X, X, Y). % Y is X squared if X times X is Y.
sqrt(X, Y) :- mult(Y, Y, X). % Y is the squareroot of X if Y times Y is X.

div(0,_,0).
div(N,succ(0),N).
div(N, N, succ(0)).
div(X, Y, succ(Z)) :- div(D, Y, Z), add(D, Y, X).

% Family Ties
%          Jane, Henry
% Sharon, John -- Kate, Dave
% Harry          Mike -- Lisa, Peter
%                           Fiona

female(jane).
female(sharon).
female(kate).
female(lisa).
female(fiona).
partner(jane, henry).
partner(sharon, john).
partner(kate, dave).
partner(lisa, peter).
child(john, jane).
child(john, henry).
child(kate, jane).
child(kate, henry).
child(harry, sharon).
child(harry, john).
child(mike, kate).
child(mike, dave).
child(lisa, kate).
child(lisa, dave).
child(fiona, lisa).
child(fiona, peter).

male(X) :- not(female(X)).
wife(X, Y) :- partner(X, Y), female(X).
husband(X, Y) :- partner(Y, X), male(X).
parent(X, Y) :- child(Y, X).
ancestor(X, Y) :- parent(X, Z), (parent(X, Y) ; (ancestor(X, D), parent(D, Y))).
sibling(X, Y) :- child(X, Z), child(Y, Z).
grandparent(X, Y) :- parent(X, Z), parent(Z, Y).

brother(X, Y) :- sibling(X, Y), male(X).
sister(X, Y) :- sibling(X, Y), female(X).
mother(X, Y) :- parent(X, Y), female(X).
father(X, Y) :- parent(X, Y), male(X).
grandma(X, Y) :- grandparent(X, Y), female(X).
grandpa(X, Y) :- grandparent(X, Y), male(X).
cousin(X, Y) :- grandparent(Z, X), grandparent(Z, Y), not(sibling(X, Y)).
uncle(X, Y) :- sibling(Z, X), parent(Z, Y), male(X). 
aunt(X, Y) :- sibling(Z, X), parent(Z, Y), female(X).